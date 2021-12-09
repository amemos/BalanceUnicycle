using amemo.balanceUnicycle.Globals;
using amemo.balanceUnicycle.structurals.events;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;


namespace amemo.balanceUnicycle.gameElements
{
    public class Collectable : LevelObject
    {
        [SerializeField]
        private List<Transform> pizzaBoxes;

        [SerializeField]
        private TextMeshPro textMeshPro;

        private int stackCount;

        public bool IsCollected = false;

        public override void Init()
        {
            objectType = ObjectType.E_COLLECTABLE;
        }

        public int GetStackCount()
        {
            return stackCount;
        }

        public void SetStackCount(int val)
        {
            this.stackCount = val;
        }

        private void EnablePizzaBoxes(int count)
        {
            textMeshPro.text = stackCount.ToString();

            if (count <= 0)
                return;

            for (int i = 0; i < count; i++)
            {
                pizzaBoxes[i].gameObject.SetActive(true);
            }
        }

        public void DisableTMPro()
        {
            textMeshPro.enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.TryGetComponent(out CharacterParent characterParent))
            {
                EnablePizzaBoxes(stackCount);
                EventManager.OnCollectableStackTrigger(this, true);
            }
        }


        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent(out CharacterParent characterParent))
            {
                EventManager.OnCollectableStackTrigger(this, false);
                if (!IsCollected)
                    EventManager.LevelFailed();
            }
        }
    }
}

