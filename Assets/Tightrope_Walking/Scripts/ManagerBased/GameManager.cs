using amemo.balanceUnicycle.singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private StackManager stackManager;
    public StackManager StackManager
    {
        get
        {
            return stackManager;
        }
        set
        {
            stackManager = value;
        }
    }

    private CharacterParent characterParent;
    public CharacterParent CharacterParent
    {
        get
        {
            return characterParent;
        }

        set
        {
            characterParent = value;
            stackManager.characterParent = value;
        }
    }



}
