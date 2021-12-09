using amemo.balanceUnicycle.gameElements;
using amemo.balanceUnicycle.structurals.Singleton;

namespace amemo.balanceUnicycle.structurals
{
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
}

