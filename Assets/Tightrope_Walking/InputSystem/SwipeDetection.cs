using amemo.balanceUnicycle.Globals;
using amemo.balanceUnicycle.structurals.events;
using UnityEngine;


namespace amemo.balanceUnicycle.structurals.inputs
{
    [DefaultExecutionOrder(-1)]
    public class SwipeDetection : MonoBehaviour
    {
        [SerializeField]
        private float minimumDistance = .2f;
        [SerializeField]
        private float maxTime = 1.0f;

        private InputManager inputManager;

        private Vector2 startPosition;
        private float startTime;

        private Vector2 endPosition;
        private float endTime;


        void Awake()
        {
            inputManager = InputManager.Instance;
        }

        private void OnEnable()
        {

            EventManager.onCollectableStackTrigger += EnableSwipeInput;
        }

        private void OnDisable()
        {

            EventManager.onCollectableStackTrigger -= EnableSwipeInput;
        }
        public void EnableSwipeInput(LevelObject levelObject, bool entered)
        {
            if (entered)
            {
                inputManager.onStart += SwipeStart;
                inputManager.onEnd += SwipeEnd;
            }
            else
            {
                inputManager.onStart -= SwipeStart;
                inputManager.onEnd -= SwipeEnd;
            }

        }


        private void SwipeStart(Vector2 position, float time)
        {
            startPosition = position;
            startTime = time;
        }


        private void SwipeEnd(Vector2 position, float time)
        {
            endPosition = position;
            endTime = time;
            DetectSwipe();
        }

        private void DetectSwipe()
        {
            if (Vector3.Distance(startPosition, endPosition) >= minimumDistance && (endTime - startTime) <= maxTime)
            {
                EventManager.OnSwipe(endPosition - startPosition);
                Debug.DrawLine(startPosition, endPosition, Color.red, 5f);
            }
        }
    }
}

