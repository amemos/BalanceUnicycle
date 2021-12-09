using amemo.balanceUnicycle.Globals;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace amemo.balanceUnicycle.structurals.events
{
    /// <summary>
    ///  EventManager was designed with Observer Pattern rules. when added event were triggered, only somewhere interested with the event are notified.
    ///  By using c# Action property, one or more parameter was passed to subscribers was provided.
    ///  
    ///  created by: Ahmet Şentürk
    /// </summary>
    public class EventManager
    {
        public static Action onReactBack;
        public static Action<LevelObject, bool> onCollectableStackTrigger;
        public static Action<Vector2> onSwipe;
        public static Action<float> onIndicatorUpdate;
        public static Action<int> onLevelStarted;
        public static Action onLevelCompleted;
        public static Action onLevelFailed;
        public static Action onLevelEnded;


        public static void OnCollectableStackTrigger(LevelObject levelObject, bool entered)
        {
            if (onCollectableStackTrigger != null) onCollectableStackTrigger(levelObject, entered);
        }

        public static void OnSwipe(Vector2 vector2)
        {
            if (onSwipe != null) onSwipe(vector2);
        }

        public static void LevelStarted(int level)
        {
            if (onLevelStarted != null) onLevelStarted(level);
        }

        public static void UpdateIndicator(float degree)
        {
            if (onIndicatorUpdate != null) onIndicatorUpdate(degree);
        }

        public static void LevelCompleted()
        {
            if (onLevelCompleted != null) onLevelCompleted();
        }

        public static void LevelFailed()
        {
            if (onLevelFailed != null) onLevelFailed();
        }

        public static void LevelEnded()
        {
            if (onLevelEnded != null) onLevelEnded();

        }


    }

}

