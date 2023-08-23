using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll_Controller : MonoBehaviour
{
    [SerializeField] private bool _isRagdoll = false;
    private void Start()
    {
        Rigidbody[] _rb = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rb in _rb)
        {
            rb.isKinematic = !_isRagdoll;
        }
    }
}
