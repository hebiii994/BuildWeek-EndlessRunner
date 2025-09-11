using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnableObject
{
    public string poolTag; 
    [Range(0f, 1f)]
    public float spawnChance = 0.7f;
}

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private List<SpawnableObject> _spawnableObjects = new List<SpawnableObject>();
    [SerializeField] private List<Transform> _spawnPoints = new List<Transform>();

    private void OnEnable()
    {
        TrySpawnObject();
    }

    private void TrySpawnObject()
    {
        if (_spawnPoints.Count == 0 || _spawnableObjects.Count == 0) return;

        // Scegliamo un punto di spawn a caso
        int spawnIndex = Random.Range(0, _spawnPoints.Count);
        Transform spawnPoint = _spawnPoints[spawnIndex];

        // Scegliamo un tipo di oggetto da spawnare a caso
        SpawnableObject objectToTry = _spawnableObjects[Random.Range(0, _spawnableObjects.Count)];

        // Lanciamo i dadi: se il numero casuale è inferiore alla probabilità, generiamo l'oggetto
        if (Random.value < objectToTry.spawnChance)
        {
            // Chiediamo l'oggetto all'Object Pooler usando il tag corretto
            GameObject spawnedObject = ObjectPooler.Instance.SpawnFromPool(objectToTry.poolTag, spawnPoint.position, spawnPoint.rotation);

            // Lo impostiamo come figlio del pezzo di strada, così sparisce con lui
            if (spawnedObject != null)
            {
                spawnedObject.transform.SetParent(transform);
            }
        }
    }
}
