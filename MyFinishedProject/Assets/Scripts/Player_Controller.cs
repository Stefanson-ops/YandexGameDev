using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player_Controller : MonoBehaviour
{
    public static Action onDeath;

    [Header("Components")]
    [SerializeField] private GameObject _player;
    [SerializeField] private Rigidbody _rb;

    [Header("Values")]
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private float _minDistanceToFloor = 1f;
    [SerializeField] private float _extraGravity = 8f;

    [Header("Bools")]
    [SerializeField] private bool _onFloor = false;
    [SerializeField] private bool _isDead = false;

    private void Awake()
    {
        Initialize();
    }

    private void Update()
    {
        if (!_isDead)
        {
            MyInput();
            CheckForFloor();
            AddExtraForces();
        }
        else
            print("Player is dead");
    }

    private void Initialize()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _rb = _player.GetComponent<Rigidbody>();
    }

    private void MyInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
    }

    private void Jump()
    {
        if (_onFloor)
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);

        Vector3 randomTorque = new Vector3(UnityEngine.Random.Range(-1, 1), UnityEngine.Random.Range(-1, 1), UnityEngine.Random.Range(-1, 1));
        _rb.AddTorque(randomTorque * _jumpForce, ForceMode.Impulse);
    }

    private void CheckForFloor()
    {
        _onFloor = Physics.Raycast(_player.transform.position, Vector3.down, _minDistanceToFloor);
    }

    private void AddExtraForces()
    {
        _rb.AddForce(Vector3.down * _extraGravity, ForceMode.Force);
    }

    public void Death()
    {
        _isDead = true;
        _rb.isKinematic = true;
        Invoke(nameof(NotifyOtherScripts), 2f);
    }

    private void NotifyOtherScripts()
    {
        onDeath?.Invoke();
    }

    public void ActivatePlayer()
    {
        _isDead = false;
        _rb.isKinematic = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dead Zone"))
        {
            Death();
            print("Death from Trigger");
        }
    }
}
