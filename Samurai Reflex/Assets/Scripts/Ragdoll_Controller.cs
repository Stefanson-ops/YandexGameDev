using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll_Controller : MonoBehaviour
{
    [SerializeField] private bool _isRagdoll = false;
    [SerializeField] private Animator _anim;
    private void Start()
    {
        Rigidbody[] _rb = GetComponentsInChildren<Rigidbody>();
        _anim = GetComponentInChildren<Animator>();
        _anim.enabled = !_isRagdoll;

        foreach (Rigidbody rb in _rb)
        {
            rb.isKinematic = !_isRagdoll;
        }
    }
}
