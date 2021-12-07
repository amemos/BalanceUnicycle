using amemo.balanceUnicycle.Globals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace amemo.balanceUnicycle.gameElements
{
    public class StackOnWay : LevelObject
    {
        [SerializeField]
        private List<Transform> pizzaBoxes;

        private int stackCount;
        public override void Init()
        {
            objectType = ObjectType.E_COLLECTABLE;

            stackCount = Random.Range(-8, 9);

            EnablePizzaBoxes(stackCount);
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


    }

}
