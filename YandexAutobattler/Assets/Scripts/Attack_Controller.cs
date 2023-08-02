using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Controller : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    [SerializeField] private int _damage;
    [SerializeField] private float _attackDelay;
    [SerializeField] private float _attackRange;
    [SerializeField] private LayerMask _whatIsEnemy;

    private void Start()
    {
        StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        float startTime = Time.time;

        while (Time.time < startTime + _attackDelay)
            yield return null;

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, _attackRange, _whatIsEnemy);
        foreach (Collider2D enemy in hitEnemies)
            enemy.GetComponent<Enemy_Stats>().TakeDamage(_damage);
        _anim.SetTrigger("Attack");
        print("Attack");
        StartCoroutine(Attack());
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position, _attackRange);
    }

}
