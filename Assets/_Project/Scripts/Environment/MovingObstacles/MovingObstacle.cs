    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class MovingObstacle : AbstractMover
    {
    [SerializeField] private float playerDetectionRange = 5f; // Distanza di rilevamento del giocatore
    [SerializeField] protected Transform[] targetLanes;
    private int currentLaneIndex = 0; // Indice della corsia corrente
    private int targetLaneIndex; // Indice della corsia target
    private Vector3 startPosition; // Posizione iniziale
    private Vector3 targetPosition; // Posizione target    
    
    private GameObject player; // Riferimento al giocatore
    
    protected void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogWarning("Player object not found in MovingDefensors.");
        }
    }
    
    protected void OnEnable()
    {
        if (obstacleMesh == null)
        {
            Debug.LogWarning("Obstacle mesh not assigned in MovingDefensors.");
            return;
        }
        startPosition = obstacleMesh.transform.position;
        if (targetLanes == null || targetLanes.Length == 0)
        {
            Debug.LogError("Missing the Lanes prefab!");
            return;
        }
        SetNewTarget();
    }

    public override void Move()
    {
        obstacleMesh.transform.position = Vector3.MoveTowards(obstacleMesh.transform.position, targetPosition, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(obstacleMesh.transform.position, targetPosition) < 0.1f)
        {
            //_hasMovedToTarget = true;
            currentLaneIndex = targetLaneIndex; // Aggiorna la corsia corrente
            SetNewTarget();
        }
        if (!_hasMovedToTarget)
        {
            //if (Vector3.Distance(obstacleMesh.transform.position, player.transform.position) < playerDetectionRange)
            //{
            //    Debug.Log("It Was Me, Dio!");   
            //}
        }
    }

    // Metodo per cambiare target (opzionale)
    public void SetNewTarget()
    {
        //_hasMovedToTarget = false; // Reset dello stato

        if (targetLanes != null && targetLanes.Length > 0)
        {
            // Allinea le corsie alla posizione iniziale dell'ostacolo
            foreach (Transform lane in targetLanes)
            {
                float yPos = startPosition.y;
                lane.transform.position = new Vector3(lane.transform.position.x, yPos, lane.transform.position.z);
            }

            targetLaneIndex = Random.Range(0, targetLanes.Length);

            if (currentLaneIndex == targetLaneIndex || Vector3.Distance(obstacleMesh.transform.position, targetPosition) < 0.1f)
            {
                targetLaneIndex = (targetLaneIndex + 1) % targetLanes.Length; // Cambia corsia se è la stessa
            }

            Debug.Log($"Target position set to: {targetLanes[targetLaneIndex]}");
        }
        else
        {
            Debug.LogWarning("Lanes positions not set in MovingDefensors.");
            targetLaneIndex = 0;
        }
        targetPosition = targetLanes[targetLaneIndex].transform.position; // Imposta la posizione target
    }
}