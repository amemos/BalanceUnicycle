using System;
using UnityEngine;

namespace amemo.balanceUnicycle
{
    public abstract class BaseMonoBehaviour : MonoBehaviour
    {
        public Action<BaseMonoBehaviour> OnActivated;
        public Action<BaseMonoBehaviour> OnDeactivated;
        public Action<BaseMonoBehaviour> OnDestroyed;

        public void SetActive(bool isActive)
        {
            if (isActive)
            {
                gameObject.SetActive(isActive);
                OnActivatedCallback();
            }
            else
            {
                OnDeactivatedCallback();
                gameObject.SetActive(isActive);
            }
        }

        public void SetName(string name)
        {
            gameObject.name = name;
        }


        public virtual void OnActivatedCallback()
        {
            OnActivated?.Invoke(this);
        }

        public virtual void OnDeactivatedCallback()
        {
            OnDeactivated?.Invoke(this);
        }

        public virtual void Destroy()
        {
            Destroy(gameObject);
        }

        public virtual void Destroy(float time)
        {
            Destroy(gameObject, time);
        }

        #region Unity Event Functions //////////////////////////////////////////

        protected virtual void OnDestroy()
        {
            OnDestroyed?.Invoke(this);
        }

        #endregion
    }
}