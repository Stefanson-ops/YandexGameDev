using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Controller : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _projectileSpeed;

    private void Update()
    {
        MovingToTarget();
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private void MovingToTarget()
    {
        transform.position = Vector3.Lerp(transform.position, _target.position, _projectileSpeed * Time.deltaTime);
        transform.LookAt(_target);
    }

    private void OnCollisionEnter(Collision collision)
    {
        print(collision.collider.name);
        collision.collider.GetComponentInParent<Player_Controller>().Death("Death From Sky");
        Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        Destroy(gameObject);
    }
}
