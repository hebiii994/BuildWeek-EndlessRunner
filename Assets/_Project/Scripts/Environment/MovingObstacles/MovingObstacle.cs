    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class MovingObstacle : AbstractMover
    {
        [SerializeField] private float playerDetectionRange = 5f; // Distanza di rilevamento del giocatore
        [SerializeField] protected Transform[] targetPos;
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
            SetNewTarget();
        }

    //La funzione di Update deve essere cancellata in fase finale perché la funzione testFunction è appunto
    //Una funzione solo di testing
        public override void Update()
        {
            base.Update();
            TestFunction();
        }

        public void TestFunction()
        {
            if (Input.GetButton("Jump"))
            {
                StartSimpleMovement();
            }
            else if (Input.GetButtonDown("Fire1"))
            {
                SetNewTarget();
            }
        }

        public override void Move()
        {
            if (!_hasMovedToTarget && player != null)
            {
                if (Vector3.Distance(obstacleMesh.transform.position, player.transform.position) < playerDetectionRange)
                {
                    transform.position = Vector3.MoveTowards(obstacleMesh.transform.position, targetPosition, moveSpeed * Time.deltaTime);
                    if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
                    {
                        _hasMovedToTarget = true;
                        currentLaneIndex = targetLaneIndex; // Aggiorna la corsia corrente
                    }
                }
            }
        }

        // Attiva il movimento semplice
        private void StartSimpleMovement()
        {
            obstacleMesh.transform.position = Vector3.MoveTowards(obstacleMesh.transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }


        // Metodo per cambiare target (opzionale)
        public void SetNewTarget()
        {
            _hasMovedToTarget = false; // Reset dello stato

            if (targetPos != null && targetPos.Length > 0)
            {
                targetLaneIndex = Random.Range(0, targetPos.Length);
                if (currentLaneIndex == targetLaneIndex || Vector3.Distance(obstacleMesh.transform.position, targetPosition) < 0.1f)
                {
                    targetLaneIndex = (targetLaneIndex + 1) % targetPos.Length; // Cambia corsia se è la stessa
                }
                targetPosition = targetPos[targetLaneIndex].position; // Imposta la posizione target

                // Allinea le corsie alla posizione iniziale dell'ostacolo
                // Sto supponendo che il player si muova nell'asse Y
                foreach (Transform lane in targetPos)
                {
                    float yPos = startPosition.y;
                    lane.transform.position = new Vector3(lane.transform.position.x, yPos, lane.transform.position.z);
                }
                Debug.Log($"Target position set to: {targetPos[targetLaneIndex]}");
            }
            else
            {
                Debug.LogWarning("Lanes positions not set in MovingDefensors.");
                targetLaneIndex = 0;
            }
        }
    }