using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpectatorController : MonoBehaviour
{
    public float _maxMoveDistance = 3f;  
    public float _moveSpeed = 2f;     

    private Vector3 _startPosition;

    void Awake()
    {
        _startPosition = transform.position;
        StartCoroutine(MoveUp());
    }

    IEnumerator MoveUp()
    {
         float _jumpHeight = Random.Range(0,_maxMoveDistance);

        Vector3 target = _startPosition + Vector3.up * _jumpHeight;

        while (Vector3.Distance(transform.position, target) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, _moveSpeed * Time.deltaTime);
            yield return null;
        }

        StartCoroutine(MoveDown());
    }

    IEnumerator MoveDown()
    {
        while (Vector3.Distance(transform.position, _startPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, _startPosition, _moveSpeed * Time.deltaTime);
            yield return null;
        }

        StartCoroutine(MoveUp());
    }
}
