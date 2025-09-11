using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideWalkSpawner : MonoBehaviour
{
    [SerializeField] private string[] _obstacleTags;
    [SerializeField] private GameObject[] _spawnPoints;

    private ObjectPooler _objectPooler;

    void OnEnable()
    {
        if (_objectPooler == null)
        {
            _objectPooler = ObjectPooler.Instance;
        }

        GenerateObstacles();
    }
    void GenerateObstacles()
    {
        foreach (var point in _spawnPoints)
        {
            if (Random.Range(0, 100) < 50) 
            {
                string randomTag = _obstacleTags[Random.Range(0, _obstacleTags.Length)];
                GameObject obstacle = _objectPooler.SpawnFromPool(randomTag, point.transform.position, Quaternion.identity);

                obstacle.transform.SetParent(transform);
            }
        }
    }
}
