using amemo.balanceUnicycle.Globals;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  EventManager was designed with Observer Pattern rules. when added event were triggered, only somewhere interested with the event are notified.
///  By using c# Action property, one or more parameter was passed to subscribers was provided.
///  
///  created by: Ahmet Şentürk
/// </summary>
public class EventManager
{
    public static Action                        onReactBack;
    public static Action<LevelObject, bool>     onWayStackTrigger;
    public static Action<int>                   onLevelStarted;
    public static Action<int>                   onLevelCompleted;
    public static Action                        onLevelFailed;
    public static Action                        onLevelEnded;


    public static void OnWayStackTrigger(LevelObject levelObject, bool entered)
    {
        if (onWayStackTrigger != null) onWayStackTrigger(levelObject, entered);
    }

    public static void LevelStarted(int level)
    {
        if (onLevelStarted != null) onLevelStarted(level);
    }

    public static void LevelCompleted(int xIndex)
    {
        if (onLevelCompleted != null) onLevelCompleted(xIndex);
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
