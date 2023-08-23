using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment_Mover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    private void Start()
    {
        Destroy(gameObject, 8);
    }

    private void Update()
    {
        transform.Translate(-Vector3.forward * _moveSpeed * Time.deltaTime);
    }
}
