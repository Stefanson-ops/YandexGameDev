using UnityEngine;
using System;
using UnityEngine.AI;

public class Enemy_Controller : MonoBehaviour
{

    [SerializeField] private NavMeshAgent _agent;

    [SerializeField] private Transform _player;
    [SerializeField] public float MinDistanceToPlayer;
    [SerializeField] public float Speed;
    [SerializeField] public float AttackSpeed;
    [SerializeField] private bool _canAttack = true;

    [SerializeField] private Enemy_Animation_Controller _animationController;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _agent.speed = Speed;

        _agent.SetDestination(_player.position);
    }

    private void Update()
    {
        MoveToPlayer();
        SetAnimations();
    }

    private void MoveToPlayer()
    {
        if (Vector2.Distance(transform.position, _player.position) > MinDistanceToPlayer)
            _agent.speed = Speed;
        else
            _agent.speed = 0;


        _agent.SetDestination(_player.position);
    }

    private void SetAnimations()
    {
        if (_agent.speed > 0)
            _animationController.SetRunningState(true);
        else
        {
            _animationController.SetRunningState(false);
            if (_canAttack)
                Attack();
        }

        if (_agent.destination.x < transform.position.x)
            _animationController.FlipSprite(true);
        else
            _animationController.FlipSprite(false);


    }

    private void Attack()
    {
        _animationController.Attack();
        _canAttack = false;
        Invoke(nameof(ReloadAttack), 1);
    }

    private void ReloadAttack()
    {
        _canAttack = true;
    }

    public void Death()
    {
        Destroy(gameObject);
        print(this.name + " death");
    }
}
