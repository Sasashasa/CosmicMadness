using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance { get; private set; }
    
    [System.Serializable]
    public class Pool 
    {
        public PoolTag Tag;
        public GameObject Prefab;
        public int Size;
    }
    
    public List<Pool> Pools;
    private Dictionary<PoolTag, Queue<GameObject>> PoolDictionary;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        PoolDictionary = new Dictionary<PoolTag, Queue<GameObject>>();

        foreach (Pool pool in Pools) 
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i=0; i < pool.Size; i++) 
            {
                GameObject obj = Instantiate(pool.Prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            PoolDictionary.Add(pool.Tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(PoolTag poolTag, Vector3 position, Quaternion rotation) 
    {
        if (!PoolDictionary.ContainsKey(poolTag))
            return null;

        GameObject objToSpawn = PoolDictionary[poolTag].Dequeue();

        objToSpawn.SetActive(true);
        objToSpawn.transform.position = position;
        objToSpawn.transform.rotation = rotation;

        PoolDictionary[poolTag].Enqueue(objToSpawn);

        return objToSpawn;
    }
}