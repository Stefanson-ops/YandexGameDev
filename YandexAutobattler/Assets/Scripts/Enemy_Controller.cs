using UnityEngine;
using System;
using UnityEngine.AI;

public class Enemy_Controller : MonoBehaviour
{
    public static Action onDeath;

    [SerializeField] private NavMeshAgent _agent;

    [SerializeField] private Transform _player;
    [SerializeField] public float _minDistanceToPlayer;
    [SerializeField] public float _speed;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _agent.speed = _speed;

        _agent.SetDestination(_player.position);
    }

    private void Update()
    {
        MoveToPlayer();
    }

    private void MoveToPlayer()
    {
        if (Vector2.Distance(transform.position, _player.position) > _minDistanceToPlayer)
            _agent.speed = _speed;
        else
            _agent.speed = 0;


        _agent.SetDestination(_player.position);
    }

    private void Death()
    {
        onDeath?.Invoke();
        print(this.name + " death");
    }

    private void OnDestroy()
    {
        Death();
    }
}
