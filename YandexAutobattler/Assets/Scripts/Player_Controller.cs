using UnityEngine;
using UnityEngine.AI;

public class Player_Controller : MonoBehaviour
{
    [SerializeField]private NavMeshAgent _agent;

    [SerializeField] private float _speed;
    [SerializeField] private Transform _target;
    [SerializeField] private float _minDistanceToTarget;

    private Vector2 target;

    private void Start()
    {
        FindTarget();
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _agent.speed = _speed;

        _agent.SetDestination(_target.position);
    }

    private void Update()
    {
        MovingToTarget();
    }

    private void MovingToTarget()
    {
        SpeedController();

        if (_target == null)
            _agent.SetDestination(Vector2.zero);
        else
            _agent.SetDestination(_target.position);
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

    private void FindTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        _target = enemies[Random.Range(0, enemies.Length)].transform;
        Invoke(nameof(FindTarget), 1);
    }
}
