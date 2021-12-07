using amemo.balanceUnicycle.Globals;
using System.Collections;
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

        public override void Init()
        {
            objectType = ObjectType.E_COLLECTABLE;

            stackCount = Random.Range(1, 16);

        }

        public int GetStackCount()
        {
            return stackCount;
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
            }
        }
    }
}

