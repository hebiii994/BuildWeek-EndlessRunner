using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    float _speed = 5f;
    Transform _target;
    private void Awake()
    { 
        _target = transform.parent;
    }
    private void OnEnable()
    {
        transform.localPosition = new Vector3(100f,0,0);  
    }
    private void Update()
    {
        Vector3 direction = (_target.position - transform.position).normalized;

        transform.position += direction * _speed * Time.deltaTime;
    }
}
