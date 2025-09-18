using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    [SerializeField] public string _roadTags;
    [SerializeField] private float _roadLength = 50f;
    [SerializeField] private int _numberOfRoads = 5;

    private int _totalRoadsSpawned = 0;
    private ObjectPooler _objectPooler;
    private float _zSpawn = 0f;
    private List<GameObject> _activeRoads = new List<GameObject>();
    private Transform _playerTransform;

    private BiomeManager _biomeManager;

    private void Awake()
    {
        _biomeManager = gameObject.GetComponent<BiomeManager>();//DEVE ESSERE SULLO STESSO OBJECT DI BIOMEMANAGER
        
    }
    void Start()
    {

        _objectPooler = ObjectPooler.Instance;
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _totalRoadsSpawned = 0;
        for (int i = 0; i < _numberOfRoads; i++)
        {
            SpawnRoad();
        }
    }
    public void SpawnRoad()
    {
        string tag = _biomeManager.UpdateBiome();
        GameObject road = _objectPooler.SpawnFromPool(tag, transform.forward * _zSpawn, transform.rotation);
        
        ObstacleSpawner spawner = road.GetComponent<ObstacleSpawner>();
        if (spawner != null)
        {
            if (_totalRoadsSpawned == 0)
            {
                Debug.Log("Primo blocco: nessun ostacolo.");
            }
            else
            {
                Debug.Log($"Blocco {_totalRoadsSpawned}: forzo spawn ostacoli.");
                spawner.SpawnObjectsImmediate();
            }
        }
        else
        {
            Debug.LogWarning("Nessun ObstacleSpawner trovato sul blocco!");
        }
        _biomeManager._roadSoFar++;
        _activeRoads.Add(road);
        _zSpawn += _roadLength;
        _totalRoadsSpawned++;
    }
    private void Update()
    {
        if (_playerTransform.position.z - 55 > _zSpawn - (_numberOfRoads * _roadLength))
        {
            SpawnRoad();
            DeleteRoad();
        }
    }
    private void DeleteRoad()
    {
        _activeRoads[0].SetActive(false);
        _activeRoads.RemoveAt(0);
    }
}
