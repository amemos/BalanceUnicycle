using amemo.balanceUnicycle.Globals;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


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

            stackCount = Random.Range(-16, 16);

            EnablePizzaBoxes(stackCount);

            textMeshPro.text = stackCount.ToString();
            //textMeshPro.rectTransform.position = new Vector3(0, stackCount * 0.1f * 0);
        }

        private void EnablePizzaBoxes(int count)
        {
            if (count <= 0)
                return;

            for (int i = 0; i < count; i++)
            {
                pizzaBoxes[i].gameObject.SetActive(true);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            
        }
    }
}

