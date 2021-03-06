using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using amemo.balanceUnicycle.Globals;
using amemo.balanceUnicycle.structurals.Singleton;

namespace amemo.balanceUnicycle.structurals.pooler
{
    /// <summary>
    ///  Pool-Object-Pattern provides that all game objects are created at initial. It is flexible as being expanded while necessary.
    ///  In run-time memory filling and deletion processes are blocked and avoiding memory spikes is aimed.
    ///  
    ///  created by: Ahmet Şentürk
    /// </summary>
    /// 

    public class ObjectPooler : Singleton<ObjectPooler>
    {

        public List<ObjectPoolItem> itemsToPool;

        public List<GameObject> pooledObjects;

        void Start()
        {
            pooledObjects = new List<GameObject>();
            foreach (ObjectPoolItem item in itemsToPool)
            {
                for (int i = 0; i < item.amountToPool; i++)
                {
                    GameObject obj = (GameObject)Instantiate(item.objectToPool);
                    obj.SetActive(false);
                    pooledObjects.Add(obj);
                }
            }
        }


        public GameObject GetPooledObject(ObjectType type)
        {
            for (int i = 0; i < pooledObjects.Count; i++)
            {
                if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].GetComponent<LevelObject>().GetObjectType() == type)
                {
                    return pooledObjects[i];
                }
            }
            foreach (ObjectPoolItem item in itemsToPool)
            {
                if (item.objectToPool.GetComponent<LevelObject>().GetObjectType() == type)
                {
                    if (item.shouldExpand)
                    {
                        GameObject obj = (GameObject)Instantiate(item.objectToPool);
                        obj.SetActive(false);
                        pooledObjects.Add(obj);
                        return obj;
                    }
                }
            }
            return null;
        }

        public void DestroyGameObj(GameObject go)
        {
            go.SetActive(false);
        }

    }
}

