using amemo.balanceUnicycle.Globals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace amemo.balanceUnicycle.gameElements
{
    public class Player : LevelObject
    {
        [SerializeField]
        private Animator animator;

        private void OnEnable()
        {
            EventManager.onLevelStarted         += x => animator.enabled = true;
            EventManager.onLevelCompleted       += () => animator.enabled = false;
            EventManager.onLevelFailed          += () => animator.enabled = false;
        }

        private void OnDisable()
        {
            EventManager.onLevelStarted         -= x => animator.enabled = true;
            EventManager.onLevelCompleted       -= () => animator.enabled = false;
            EventManager.onLevelFailed          -= () => animator.enabled = false;
        }

        public override void Init()
        {
            objectType = ObjectType.E_PLAYER;
        }

    }
}
