using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnableObject
{
    [Tooltip("Il tag usato nell'Object Pooler per questo oggetto.")]
    public string poolTag;

    [Tooltip("La probabilità da 0 a 1 che questo oggetto venga scelto per lo spawn.")]
    [Range(0f, 1f)]
    public float spawnChance = 0.7f;

    [Header("Trophy Settings")]
    [Tooltip("Questo oggetto può avere un trofeo associato?")]
    public bool canHaveTrophy;

    [Tooltip("Il tag del trofeo nell'Object Pooler.")]
    public string trophyPoolTag = "Trophy"; 

    [Tooltip("La probabilità da 0 a 1 che il trofeo appaia, se l'oggetto viene generato.")]
    [Range(0f, 1f)]
    public float trophyChance = 0.5f;

    [Tooltip("La posizione del trofeo relativa all'ostacolo (es. Y=2 per metterlo sopra).")]
    public Vector3 trophySpawnOffset = new Vector3(0, 2f, 0);
}

public class ObstacleSpawner : MonoBehaviour
{
    [Tooltip("Lista degli oggetti che possono essere generati.")]
    [SerializeField] private List<SpawnableObject> _spawnableObjects = new List<SpawnableObject>();

    [Tooltip("Trascina qui tutti i punti di spawn figli di questo oggetto.")]
    [SerializeField] private List<Transform> _spawnPoints = new List<Transform>();

    [Tooltip("Quanti oggetti provare a generare al massimo su questo pezzo di strada.")]
    [SerializeField] private int maxObjectsToSpawn = 3;

    [Header("Ground Alignment Settings")]
    [Tooltip("Seleziona il layer 'Ground' per allineare gli oggetti al terreno.")]
    [SerializeField] private LayerMask _groundLayer;

    private void Awake()
    {
        if (_spawnPoints.Count > 0) return;

        foreach (Transform child in transform)
        {
            if (child.CompareTag("SpawnPointContainer"))
            {
                foreach (Transform point in child)
                {
                    _spawnPoints.Add(point);
                }

                Debug.Log($"Trovati e assegnati {_spawnPoints.Count} spawn points dal contenitore '{child.name}'", this.gameObject);
                return; 
            }
        }

        Debug.LogWarning($"Nessun contenitore con il tag 'SpawnPointContainer' trovato su {gameObject.name}. Assicurati di aver assegnato il tag corretto.", this.gameObject);
    }

    public void SpawnObjectsImmediate()
    {
        StartCoroutine(SpawnWithPhysicsDelay());

    }


    private IEnumerator SpawnWithPhysicsDelay()
    {
        yield return new WaitForFixedUpdate();

        if (_spawnPoints.Count == 0 || _spawnableObjects.Count == 0) yield break;

        List<Transform> availablePoints = new List<Transform>(_spawnPoints);
        int amountToSpawn = Random.Range(1, maxObjectsToSpawn + 1);

        for (int i = 0; i < amountToSpawn; i++)
        {
            if (availablePoints.Count == 0) break;
            int spawnIndex = Random.Range(0, availablePoints.Count);
            Transform spawnPoint = availablePoints[spawnIndex];
            availablePoints.RemoveAt(spawnIndex);
            TrySpawnObjectAtPoint(spawnPoint);
        }
    }



    private void TrySpawnObjectAtPoint(Transform spawnPoint)
    {
        if (_spawnableObjects.Count == 0) return;

        SpawnableObject objectToTry = _spawnableObjects[Random.Range(0, _spawnableObjects.Count)];

        if (Random.value < objectToTry.spawnChance)
        {
            Vector3 rayStartPoint = spawnPoint.position + Vector3.up * 5f;
            RaycastHit hit;

            if (Physics.Raycast(rayStartPoint, Vector3.down, out hit, 10f, _groundLayer, QueryTriggerInteraction.Collide))
            {
                Vector3 spawnPosition = hit.point;
                Quaternion spawnRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
                GameObject spawnedObject = ObjectPooler.Instance.SpawnFromPool(objectToTry.poolTag, spawnPosition, spawnRotation);

                if (spawnedObject != null)
                {
                    spawnedObject.transform.SetParent(transform);
                    if (objectToTry.canHaveTrophy && Random.value < objectToTry.trophyChance)
                    {
                        Vector3 trophyPosition = spawnedObject.transform.position + spawnedObject.transform.TransformDirection(objectToTry.trophySpawnOffset);
                        GameObject spawnedTrophy = ObjectPooler.Instance.SpawnFromPool(objectToTry.trophyPoolTag, trophyPosition, Quaternion.identity);
                        if (spawnedTrophy != null)
                        {
                            spawnedTrophy.transform.SetParent(transform);
                        }
                    }
                }
            }
            else
            {
                Debug.LogError($"<color=red>[{gameObject.name}] Raycast FALLITO anche con QueryTriggerInteraction.Collide! Controlla la geometria del prefab della strada.</color>", this.gameObject);
            }
        }
    }
}