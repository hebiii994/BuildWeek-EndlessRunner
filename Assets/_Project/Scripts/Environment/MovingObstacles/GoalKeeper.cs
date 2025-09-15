using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GoalKeeper : AbstractMover
{
    [SerializeField] private float _timeToStartMoving = 3f;
    [SerializeField] private float _timeToFinishMoving = 2.5f;
    [SerializeField] private float _maxDistance = 5f;

    private bool shouldMove = false;
    private float xDestination;

    private void OnEnable()
    {
        xDestination = obstacleMesh.transform.position.x + _maxDistance;
        Invoke("ShouldMove", _timeToStartMoving);
    }

    public void ShouldMove()
    {
        if (!_hasMovedToTarget)
            shouldMove = true;
        else 
            shouldMove = false;
    }
    public override void Move()
    {
        if (!shouldMove || _hasMovedToTarget) return;

        float xPos = Mathf.MoveTowards(obstacleMesh.transform.position.x, xDestination, moveSpeed * Time.deltaTime);
        obstacleMesh.transform.position = new Vector3(xPos, transform.position.y, transform.position.z);

        if (Mathf.Approximately(obstacleMesh.transform.position.x, xDestination))
        {
            _hasMovedToTarget = true;
            shouldMove = false;
        }
    }

}
