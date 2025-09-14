using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BenchManager : MonoBehaviour
{
    [SerializeField] private List<Transform> SpawnPoints = new List<Transform>();
    [SerializeField] private List<GameObject> SpawnObjects = new List<GameObject>();
    [SerializeField] private Vector3 _spawnPointOffset;

    private void Start()
    {
        for (int i = 0; i <= SpawnPoints.Count -1; i++)
        {
            int random = Random.Range(0, 3);

            Transform spCache = null;

            try
            {
                spCache = SpawnPoints[i];
            }
            catch (System.Exception e)
            {
                Debug.LogWarning($"Indice{i} fuori range {e}");
            }

            if (random == 0)
            {
                spCache.gameObject.SetActive(false);
            }
            else if (random >= 1)
            {
                Vector3 _spawnpointPos = spCache.transform.position + _spawnPointOffset;


                Instantiate(SpawnObjects[Random.Range(0,SpawnObjects.Count)], _spawnpointPos, spCache.transform.rotation, spCache);
            }
        }
    }
}
