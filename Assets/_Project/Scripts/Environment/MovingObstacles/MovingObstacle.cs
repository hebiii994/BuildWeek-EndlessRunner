using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : AbstractMover
{
    [SerializeField] private float _timeBeforeNextMove = 2f;
    [SerializeField] protected Transform[] targetLanes;
    private int currentLaneIndex = 0;
    private int targetLaneIndex;
    private Vector3 targetLocalPosition;

    private float waitTimer = 0f;
    private bool isWaiting = false;

    protected void Awake()
    {
        if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            Debug.LogWarning("Player non trovato.");
        }
    }

    protected void OnEnable()
    {
        if (obstacleMesh == null)
        {
            Debug.LogError("obstacleMesh non assegnato.");
            return;
        }

        if (targetLanes == null || targetLanes.Length == 0)
        {
            Debug.LogError("targetLanes non assegnato.");
            return;
        }

        SetNewTarget();
    }

    public override void Move()
    {
        if (isWaiting) //Attesa quando raggiunge la destinazione
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= _timeBeforeNextMove)
            {
                isWaiting = false;
                waitTimer = 0f;
                SetNewTarget();
            }
            return;
        }

        obstacleMesh.transform.localPosition = Vector3.MoveTowards(obstacleMesh.transform.localPosition, targetLocalPosition, moveSpeed * Time.deltaTime);

        //Raggiunto il target
        if (Vector3.Distance(obstacleMesh.transform.localPosition, targetLocalPosition) < 0.05f)
        {
            currentLaneIndex = targetLaneIndex;
            isWaiting = true;
            waitTimer = 0f;
        }
    }

    private void SetNewTarget()
    {
        if (targetLanes.Length == 1)
        {
            targetLaneIndex = 0;
        }
        else
        {
            int newIndex;          
            newIndex = Random.Range(0, targetLanes.Length);
            
            if (newIndex == currentLaneIndex)
                targetLaneIndex = Random.Range(0, targetLanes.Length); //Mi assicuro che non setti sè stesso            
        }

        Vector3 laneLocalPos = targetLanes[targetLaneIndex].localPosition;

        targetLocalPosition = new Vector3(laneLocalPos.x, obstacleMesh.transform.localPosition.y, obstacleMesh.transform.localPosition.z);

        Debug.Log($"Nuovo target: corsia {targetLaneIndex}, localPosition {targetLocalPosition}");
    }
}