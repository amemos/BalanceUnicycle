using amemo.balanceUnicycle.Globals;
using amemo.balanceUnicycle.structurals;
using amemo.balanceUnicycle.structurals.events;
using System.Collections.Generic;
using UnityEngine;

namespace amemo.balanceUnicycle.gameElements
{
    public class CharacterParent : LevelObject
    {
        [SerializeField]
        private float speed = 1.0f;

        public List<Transform> rightStack;
        public List<Transform> leftStack;
        public Transform stick;

        private bool levelStarted = false;

        protected override void Awake()
        {
            GameManager.Instance.CharacterParent = this;
        }

        private void OnEnable()
        {
            EventManager.onLevelStarted += x => levelStarted = true;
            EventManager.onLevelFailed += () => levelStarted = false;
            EventManager.onLevelCompleted += () => levelStarted = false;
        }

        private void OnDisable()
        {
            EventManager.onLevelStarted -= x => levelStarted = true;
            EventManager.onLevelFailed -= () => levelStarted = false;
            EventManager.onLevelCompleted -= () => levelStarted = false;
        }

        private void Update()
        {
            if (levelStarted) transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }

        public override void Init()
        {
            SetInitialPosition(transform.position);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out LevelEnd levelEnd))
            {
                EventManager.LevelCompleted();
            }
        }


    }

}
