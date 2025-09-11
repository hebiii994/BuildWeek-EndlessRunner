using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pool
{
    public string tag; // Un nome per identificare la pool 
    public List<GameObject> prefab = new List<GameObject>();
    public int size; // Il numero di oggetti da creare all'inizio
}

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    void Awake()
    {
        Instance = this;
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab[Random.Range(0,pool.prefab.Count)]);
                obj.SetActive(false); 
                objectPool.Enqueue(obj); 
            }

            poolDictionary.Add(pool.tag, objectPool);
        }

    }
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("La Pool con il tag " + tag + " non esiste.");
            return null;
        }


        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}
