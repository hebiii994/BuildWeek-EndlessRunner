using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideWalkSpawner : MonoBehaviour
{
    [SerializeField]private List<GameObject> BuildingsPrefabs = new List<GameObject>();

    private void Awake()
    {
        RandomBuilding();
    }
    public void RandomBuilding()
    {
     var building = Instantiate(BuildingsPrefabs[Random.Range(0,BuildingsPrefabs.Count)],transform.position,Quaternion.identity);
    }
}
