using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractMover : MonoBehaviour
{
    [SerializeField] protected float moveSpeed = 5f; // Velocità di movimento

    [SerializeField] protected Transform[] targetPos;

    [SerializeField] public GameObject obstacleMesh { get; set; } // Mesh dell'ostacolo

    public virtual void Move()
    {
    }

}
