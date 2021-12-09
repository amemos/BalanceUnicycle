using amemo.balanceUnicycle.Globals;
using amemo.balanceUnicycle.structurals;
using amemo.balanceUnicycle.structurals.events;
using System.Collections.Generic;
using UnityEngine;

namespace amemo.balanceUnicycle.gameElements
{
    [DefaultExecutionOrder(-2)]
    public class Character : LevelObject
    {
        [SerializeField]
        private Animator animator;
        [SerializeField]
        private float angleThreshold = 15.0f;

        private List<AnimationStates> animationStates;

        private void OnEnable()
        {
            EventManager.onLevelStarted         += x => animator.enabled = true;
            EventManager.onLevelCompleted       += OnFinish;
            EventManager.onLevelFailed          += OnFinish;
            EventManager.onIndicatorUpdate      += ChangePos;
        }

        private void OnDisable()
        {
            EventManager.onLevelStarted         -= x => animator.enabled = true;
            EventManager.onLevelCompleted       -= OnFinish;
            EventManager.onLevelFailed          -= OnFinish;
            EventManager.onIndicatorUpdate      -= ChangePos;
        }

        protected override void Awake()
        {
            base.Awake();
            GameManager.Instance.Player = this;
        }

        private void ChangePos(float angle)
        {
            if(angle > angleThreshold)
            {
                SetPlayerAnimation(AnimationStates.SkiPoseRight);
            }
            else if(angle < -angleThreshold)
            {
                SetPlayerAnimation(AnimationStates.SkiPoseLeft);
            }
            else
            {
                SetPlayerAnimation(AnimationStates.Ski);
            }
               
        }

        public override void Init()
        {
            animationStates = new List<AnimationStates>() { AnimationStates.Ski, AnimationStates.SkiPoseRight, AnimationStates.SkiPoseLeft };
            objectType = ObjectType.E_PLAYER;
        }

        private void SetPlayerAnimation(AnimationStates animation)
        {
            foreach (var anim in animationStates)
            {
                animator.SetBool(anim.ToString(), false);
            }
            animator.SetBool(animation.ToString(), true);
        }

        private void OnFinish()
        {
            animator.enabled = false;
            enabled = false;
        }

    }
}
