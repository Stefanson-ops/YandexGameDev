using System;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Player_Controller : MonoBehaviour
{

    [SerializeField] private NavMeshAgent _agent;

    [SerializeField] private float _speed;
    [SerializeField] private Transform _target;
    [SerializeField] private float _minDistanceToTarget;

    [SerializeField] Player_Animation_Controller _animationController;
    [SerializeField] Attack_Controller _attackController;

    private Vector2 target;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _agent.speed = _speed;
        _attackController = GetComponent<Attack_Controller>();
        FindTarget();
    }

    private void Update()
    {
        MovingToTarget();
        SetAnimations();

        if (Input.GetKey(KeyCode.Space))
            Time.timeScale = 100;
        else
            Time.timeScale = 1;
    }

    private void MovingToTarget()
    {
        SpeedController();
        
        _agent.SetDestination(target);
    }

    private void SpeedController()
    {
        if (_target != null)
            target = _target.position;
        else
            target = Vector2.zero;

        if (Vector2.Distance(transform.position, target) > _minDistanceToTarget)
            _agent.speed = _speed;
        else
            _agent.speed = 0;
    }

    private void SetAnimations()
    {
        if (_agent.speed > 0)
            _animationController.SetRunningState(true);
        else
        {
            _animationController.SetRunningState(false);
            if (_target != null)
            {
                _attackController.Attack();
                Invoke(nameof(FindTarget), .5f);
            }
        }

        if (_agent.destination.x < transform.position.x)
            _animationController.FlipSprite(false);
        else
            _animationController.FlipSprite(true);

    }

    public void FindTarget()
    {
        _target = GameObject.FindGameObjectWithTag("Player").transform;
    }
}
