using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace amemo.balanceUnicycle.Globals
{
    public enum ObjectType
    {
        E_PLAYER,
        E_PLATFORM,
        E_OBSTACLE,
        E_COLLECTABLE,
        E_PIZZA_BOX,
        E_LEVELSTART,
        E_LEVELEND
    }


    public enum StackSide
    {
        E_LEFT,
        E_RIGHT
    }

    public enum AnimationStates
    {
        Ski,
        SkiPoseRight,
        SkiPoseLeft
    }

}

