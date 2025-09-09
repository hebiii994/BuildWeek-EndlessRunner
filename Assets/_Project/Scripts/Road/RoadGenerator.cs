using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject triggerPoint;
    [SerializeField] private float offset;

    [SerializeField] private GameObject roadPrefab;

    public void PlaceRoad()
    {
        var n = Instantiate(roadPrefab,new Vector3(spawnPoint.position.x, spawnPoint.position.y, spawnPoint.position.z + offset),Quaternion.identity);

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
