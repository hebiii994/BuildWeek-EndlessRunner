using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SecondarySpawn
{
    [Tooltip("Il tag dell'oggetto secondario nell'Object Pooler (es. 'Trophy', 'InvincibilityPowerUp').")]
    public string poolTag;

    [Tooltip("La probabilità da 0 a 1 che questo oggetto appaia insieme all'ostacolo principale.")]
    [Range(0f, 1f)]
    public float spawnChance = 0.5f;

    [Tooltip("La posizione dell'oggetto relativa all'ostacolo (es. Y=2 per metterlo sopra).")]
    public Vector3 spawnOffset = new Vector3(0, 2f, 0);
}


[System.Serializable]
public class SpawnableObject
{
    [Tooltip("Il tag usato nell'Object Pooler per questo oggetto.")]
    public string poolTag;

    [Tooltip("La probabilità da 0 a 1 che questo oggetto venga scelto per lo spawn.")]
    [Range(0f, 1f)]
    public float spawnChance = 0.7f;

    [Tooltip("Il raggio usato per controllare se c'è già un altro ostacolo in questa posizione.")]
    public float checkRadius = 1f;

    [Header("Secondary Spawns")]
    [Tooltip("Lista di oggetti secondari (trofei, power-up) che possono apparire con questo ostacolo.")]
    public List<SecondarySpawn> secondarySpawns = new List<SecondarySpawn>();
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

    [Header("Overlap Prevention Settings")]
    [Tooltip("Seleziona il layer a cui appartengono gli ostacoli per evitare sovrapposizioni.")]
    [SerializeField] private LayerMask _obstacleLayer;


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

            if (Physics.Raycast(rayStartPoint, Vector3.down, out hit, 10f, _groundLayer, QueryTriggerInteraction.Ignore))
            {
                if (Physics.CheckSphere(hit.point, objectToTry.checkRadius, _obstacleLayer))
                {
                    return;
                }

                GameObject spawnedObject = ObjectPooler.Instance.SpawnFromPool(objectToTry.poolTag, hit.point, Quaternion.identity);

                if (spawnedObject != null)
                {
                    Vector3 finalPosition = hit.point;
                    Quaternion finalRotation = Quaternion.FromToRotation(Vector3.up, hit.normal) * spawnedObject.transform.rotation;

                    Collider objectCollider = spawnedObject.GetComponent<Collider>();
                    if (objectCollider != null)
                    {
                        finalPosition += hit.normal * objectCollider.bounds.extents.y;
                    }

                    spawnedObject.transform.position = finalPosition;
                    spawnedObject.transform.rotation = finalRotation;
                    spawnedObject.transform.SetParent(transform);

                    var potentialSecondaries = new List<SecondarySpawn>(objectToTry.secondarySpawns);
                    potentialSecondaries.Shuffle();

                    foreach (var secondary in potentialSecondaries)
                    {
                        if (Random.value < secondary.spawnChance)
                        {
                            GameObject spawnedSecondary = ObjectPooler.Instance.SpawnFromPool(secondary.poolTag, Vector3.zero, Quaternion.identity);
                            if (spawnedSecondary != null)
                            {
                                spawnedSecondary.transform.SetParent(spawnedObject.transform);
                                spawnedSecondary.transform.localPosition = secondary.spawnOffset;
                                spawnedSecondary.transform.localRotation = Quaternion.identity;

                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}