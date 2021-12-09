using System;
using System.Collections.Generic;
using UnityEngine;
using amemo.balanceUnicycle.Globals;
using amemo.balanceUnicycle.gameElements;
using amemo.balanceUnicycle.structurals.pooler;

namespace amemo.balanceUnicycle.levelGenerator
{
    /// <summary>
    ///  LevelGenerator aims to place level objects to right positions in the scene. All created items register themselves to a list. Therefore, destroying and creating levels get easier.
    ///  To avoid memory spikes, instead of loading a scene, objects are called from the pool. (Object-Pool-Pattern)
    ///  
    ///  created by: Ahmet Þentürk
    /// </summary>
    /// 
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField]
        private Transform referenceTr;

        [Header("Level Content")]

        [SerializeField]
        private List<LevelContent> levelContents;

        [Serializable]
        public struct LevelContent
        {
            public int PlatformLength;
            public int CollectableStackCount;
        }

        [Header("Created Level Objects")]

        private LevelObject platform;
        public LevelObject Platform { get { return platform; } set { platform = value; RegisterObjList(value); } }

        private LevelObject levelStart;
        public LevelObject LevelStart { get { return levelStart; } set { levelStart = value; RegisterObjList(value); } }

        private LevelObject levelEnd;
        public LevelObject LevelEnd { get { return levelEnd; } set { levelEnd = value; RegisterObjList(value); } }

        private LevelObject collectableStack; 

        [SerializeField]
        private List<LevelObject> levelObjects;

        private int levelIndex = 0;

        List<int> dummyCollectableStackCount = new List<int> { 7, 9, -3, 11, -8, 12, 8, 9, -5, -6, 12, -7, 13, -10, 8, -2 };

        private void OnEnable()
        {
            //CreateLevel will be subscribed to an action
            //DestroyLevel will be subscribed to an action
        }

        private void OnDisable()
        {
            //CreateLevel will be unsubscribed to an action
            //DestroyLevel will be unsubscribed to an action
        }

        private void Start()
        {
            levelObjects = new List<LevelObject>();

            LevelStart = ObjectPooler.Instance.GetPooledObject(ObjectType.E_LEVELSTART).GetComponent<LevelObject>();
            Platform = ObjectPooler.Instance.GetPooledObject(ObjectType.E_PLATFORM).GetComponent<LevelObject>();
            LevelEnd = ObjectPooler.Instance.GetPooledObject(ObjectType.E_LEVELEND).GetComponent<LevelObject>();

            CreateLevel(levelIndex);

        }


        private void CreateLevel(int level)
        {
            int PlatformLength = levelContents[levelIndex].PlatformLength;
            int StackCount = levelContents[levelIndex].CollectableStackCount;
            float Distance = (float)(PlatformLength * 0.5f * 0.9f) / (float)StackCount;

            LevelStart.SetPosition(referenceTr.position + Vector3.down * 1.5f);
            Platform.transform.localScale = new Vector3(0.1f, PlatformLength * 0.25f, 0.1f);
            Platform.SetPosition(referenceTr.position + (PlatformLength / 4) * Vector3.forward);
            LevelEnd.SetPosition(LevelStart.transform.position + (PlatformLength / 2) * Vector3.forward);

            for (int i = 0; i < StackCount; i++)
            {
                collectableStack = ObjectPooler.Instance.GetPooledObject(ObjectType.E_COLLECTABLE).GetComponent<LevelObject>();
                collectableStack.SetPosition(referenceTr.position + Vector3.forward * Distance * (i + 1) + Vector3.down * 3.0f);
                RegisterObjList(collectableStack);
                collectableStack.GetComponent<Collectable>().SetStackCount(dummyCollectableStackCount[i]);
            }

        }


        private void RegisterObjList(LevelObject value)
        {
            levelObjects.Add(value);
        }

        private void DestroyLevelObject(LevelObject levelObject)
        {
            ObjectPooler.Instance.DestroyGameObj(levelObject.gameObject);
        }

        public void DestroyLevel()
        {
            foreach (var obj in levelObjects)
            {
                DestroyLevelObject(obj);
            }
        }

    }


}


