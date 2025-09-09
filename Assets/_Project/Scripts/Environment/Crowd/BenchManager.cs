using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BenchManager : MonoBehaviour
{
    [SerializeField] private List<Transform> SpawnPoints = new List<Transform>();
    [SerializeField] private GameObject _spectator;
    [SerializeField] private float _spawnPointOffset;

    private void Awake()
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
                SpawnPoints[i].gameObject.SetActive(false);
            }
            else if (random >= 1)
            {
                Vector3 _spawnpointPos = new Vector3(SpawnPoints[i].transform.position.x,
                    SpawnPoints[i].transform.position.y + _spawnPointOffset,
                    SpawnPoints[i].transform.position.z);


                Instantiate(_spectator, _spawnpointPos, Quaternion.identity);
            }
        }
    }
}
