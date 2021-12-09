using amemo.balanceUnicycle.gameElements;
using amemo.balanceUnicycle.structurals.Singleton;
using UnityEngine;

namespace amemo.balanceUnicycle.structurals
{
    /// <summary>
    ///  GameManager is a singleton class. Sub-manager actions are evaluated here. Model object is initialized and memory read/write commands are managed by GameManager. 
    ///     
    ///  created by: Ahmet Þentürk
    /// </summary>
    /// 
    [DefaultExecutionOrder(-5)]
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
                stackManager.characterParent = characterParent;
                stackManager.player = player;
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
                
            }
        }

        private Character player;

        public Character Player
        {
            get 
            {
                return player; 
            }
            set 
            { 
                player = value; 
            }
        }




    }
}

