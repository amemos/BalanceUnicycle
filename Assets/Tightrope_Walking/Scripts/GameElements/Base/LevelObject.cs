using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace amemo.balanceUnicycle.Globals
{
    public abstract class LevelObject : BaseMonoBehaviour, IInitHandler
    {
        protected ObjectType objectType;

        protected Vector3 initialPosition;

        protected Vector3 initialRotation;

        private bool firstPosAssign = true;


        public virtual ObjectType GetObjectType() { return this.objectType; }

        public virtual void SetObjectType(ObjectType objectType) { this.objectType = objectType; }

        public abstract void Init();

        public virtual void SetInitialPosition(Vector3 pos) { this.initialPosition = pos; }

        public virtual Vector3 GetInitialPosition() { return initialPosition; }

        protected virtual void Awake() { Init(); }


        public void SetPosition(Vector3 vector)
        {
            transform.position = vector;

            if (firstPosAssign)
            {
                SetInitialPosition(vector);
                gameObject.SetActive(true);
                firstPosAssign = false;
            }
        
        }

    }

    public interface IInitHandler
    {
        void Init();
    }

}
