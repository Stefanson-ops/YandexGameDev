using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walls_Controller : MonoBehaviour
{
    [SerializeField] private Transform _targetToFollow;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        _targetToFollow = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        FollowToPlayer();
    }

    private void FollowToPlayer()
    {
        if (_targetToFollow != null)
            transform.position = _targetToFollow.position;
        else return;
    }
}
