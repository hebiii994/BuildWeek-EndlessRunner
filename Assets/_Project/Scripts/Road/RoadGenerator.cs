using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    [SerializeField] private float _roadLength = 50f;
    [SerializeField] private int _numberOfRoads = 5; 

    private ObjectPooler _objectPooler;
    private float _zSpawn = 0f;
    private List<GameObject> _activeRoads = new List<GameObject>();
    private Transform _playerTransform;
    private BiomeManager _biomeManager;
    private int _totalRoadsSpawned = 0;

    void Awake()
    {
        _biomeManager = GetComponent<BiomeManager>();
    }

    void Start()
    {
        _objectPooler = ObjectPooler.Instance;
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        if (_biomeManager != null)
        {
            _biomeManager.ResetCounter();
        }

        _activeRoads.Clear();
        _zSpawn = 0f;
        _totalRoadsSpawned = 0;

        for (int i = 0; i < _numberOfRoads; i++)
        {
            SpawnRoad();
        }
    }

    void Update()
    {
        if (_playerTransform.position.z - 55 > _zSpawn - (_numberOfRoads * _roadLength))
        {
            SpawnRoad();
            DeleteRoad();
        }
    }

    public void SpawnRoad()
    {
        if (_objectPooler == null) _objectPooler = ObjectPooler.Instance;

        string tag = _biomeManager.GetCurrentBiomeTag();
        GameObject road = _objectPooler.SpawnFromPool(tag, transform.forward * _zSpawn, transform.rotation);

        _biomeManager.IncrementRoadCounter();

        if (road == null) return; 

        _activeRoads.Add(road);
        _zSpawn += _roadLength;

        // Gestione spawn ostacoli
        ObstacleSpawner spawner = road.GetComponent<ObstacleSpawner>();
        if (spawner != null && _totalRoadsSpawned > 0) 
        {
            spawner.SpawnObjectsImmediate();
        }
        _totalRoadsSpawned++;
    }

    private void DeleteRoad()
    {
        if (_activeRoads.Count == 0) return;
        _activeRoads[0].SetActive(false);
        _activeRoads.RemoveAt(0);
    }
}