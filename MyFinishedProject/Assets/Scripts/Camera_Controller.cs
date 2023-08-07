using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    [SerializeField] private float _cameraFollowSpeed;
    [SerializeField] private Transform _targetToFollow;
    [SerializeField] private Vector3 _followOffset;

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
        if(_targetToFollow!=null)
            transform.position = Vector3.Slerp(transform.position, _targetToFollow.position + _followOffset, _cameraFollowSpeed * Time.deltaTime);
        else return;
    }
}
