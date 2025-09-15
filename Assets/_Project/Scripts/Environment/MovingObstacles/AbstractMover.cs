using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractMover : MonoBehaviour
{
    [SerializeField] protected float moveSpeed = 5f; // Velocità di movimento
    [SerializeField] public GameObject obstacleMesh; // Mesh dell'ostacolo
    public bool _hasMovedToTarget { get; protected set; } = false; // Stato di movimento verso il target

    public abstract void Move();

    public virtual void Update()
    {
        Move();
    }

}
