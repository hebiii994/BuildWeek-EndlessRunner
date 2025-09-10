using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    [SerializeField] private string[] _roadTags;
    [SerializeField] private float _roadLength = 50f;
    [SerializeField] private int _numberOfRoads = 5;

    private ObjectPooler _objectPooler;
    private float _zSpawn = 0f;
    private List<GameObject> _activeRoads = new List<GameObject>();
    private Transform _playerTransform;


    void Start()
    {
        _objectPooler = ObjectPooler.Instance;
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        for (int i = 0; i < _numberOfRoads; i++)
        {
            if (i == 0)
                SpawnRoad(0);
            else
                SpawnRoad(Random.Range(0, _roadTags.Length));
        }
    }
    public void SpawnRoad(int roadIndex)
    {
        string tag = _roadTags[roadIndex];
        GameObject road = _objectPooler.SpawnFromPool(tag, transform.forward * _zSpawn, transform.rotation);

        _activeRoads.Add(road);
        _zSpawn += _roadLength;
    }
    private void Update()
    {
        if (_playerTransform.position.z - 35 > _zSpawn - (_numberOfRoads * _roadLength))
        {
            SpawnRoad(Random.Range(0, _roadTags.Length));
            DeleteRoad();
        }
    }
    private void DeleteRoad()
    {
        _activeRoads[0].SetActive(false);
        _activeRoads.RemoveAt(0);
    }
}
