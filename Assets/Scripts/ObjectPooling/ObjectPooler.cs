using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    #region Singleton
    public static ObjectPooler instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    public bool completed;
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Start()
    {
        CreatePool();
    }

    public void CreatePool()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            GameObject holder = new GameObject();
            holder.name = pool.prefab.name;

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.transform.SetParent(holder.transform);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }

        completed = true;
    }

    public GameObject SpawnFromPool(string _tag, Vector3 _position, Quaternion _rotation)
    {
        if (completed)
        {
            if (!poolDictionary.ContainsKey(_tag))
            {
                Debug.LogWarning("Pool with tag " + _tag + " doesn't exist!");
                return null;
            }

            GameObject objectToSpawn = poolDictionary[_tag].Dequeue();

            objectToSpawn.SetActive(true);
            objectToSpawn.transform.position = _position;
            objectToSpawn.transform.rotation = _rotation;

            IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>();

            if (pooledObj != null)
            {
                pooledObj.OnObjectSpawn();
            }

            poolDictionary[_tag].Enqueue(objectToSpawn);

            return objectToSpawn;
        }
        else
        {
            Debug.LogWarning("Pool hasn't completed building.");
            return null;
        }
    }
}
