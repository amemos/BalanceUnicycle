using amemo.balanceUnicycle.Globals;
using amemo.balanceUnicycle.structurals.events;
using DG.Tweening;
using UnityEngine;

namespace amemo.balanceUnicycle.gameElements
{
    public class StackElement : LevelObject
    {
        [SerializeField]
        private int stackIndex;
        [SerializeField]
        private StackSide stackSide;

        public override void Init()
        {
            objectType = ObjectType.E_PIZZA_BOX;
            SetInitialPosition(transform.position);
        }

        private void OnEnable()
        {
            EventManager.onIndicatorUpdate += UpdateSlope;
        }

        private void OnDisable()
        {
            EventManager.onIndicatorUpdate += UpdateSlope;
        }

        bool deviation;
        private void UpdateSlope(float degree)
        {
            float step = -degree / 40;

            float angle = (2 * step);
            if (Mathf.Abs(angle) > Mathf.Abs(degree))
            {
                angle = -degree;
            }
            transform.DOLocalRotate(Vector3.forward * angle, 0.2f);

        }


    }
}
