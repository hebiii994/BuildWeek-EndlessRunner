using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject triggerPoint;
    [SerializeField] private float offset;
    [SerializeField] private GameObject _roadDefaultPrefab;
    [SerializeField] private GameObject _roadRainPrefab;
    [SerializeField] private GameObject _roadSnowPrefab;

    [SerializeField] private bool isDefault;
    [SerializeField] private bool isRain;
    [SerializeField] private bool isSnow;

    public void PlaceRoad()
    {

        switch ((isDefault, isRain, isSnow))
        {
            case(true,false,false):
                Instantiate(_roadDefaultPrefab, new Vector3(spawnPoint.position.x, spawnPoint.position.y, spawnPoint.position.z + offset), Quaternion.identity);
                break;

            case(false,true,false):
                Instantiate(_roadRainPrefab, new Vector3(spawnPoint.position.x, spawnPoint.position.y, spawnPoint.position.z + offset), Quaternion.identity);
                break;

            case(false,false,true):
                Instantiate(_roadSnowPrefab, new Vector3(spawnPoint.position.x, spawnPoint.position.y, spawnPoint.position.z + offset), Quaternion.identity);
                break;

        }

          DestroyComponent(this);
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            PlaceRoad();
        }
    }
    private void DestroyComponent(RoadGenerator road)
    {
        Destroy(road);
    }
}
