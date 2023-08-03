using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Attack_Controller : MonoBehaviour
{
    public static Action OnAttack;

    [SerializeField] private Player_Animation_Controller _animController;
    [SerializeField] private int _damage;
    [SerializeField] private float _attackDelay;
    [SerializeField] private float _attackRange;
    [SerializeField] public LayerMask _whatIsEnemy;
    [SerializeField] private bool _canAttack = true;

    public void Attack()
    {
        if (_canAttack)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, _attackRange, _whatIsEnemy);
            foreach (Collider2D enemy in hitEnemies)
                enemy.GetComponent<Enemy_Stats>().TakeDamage(_damage);
            _animController.Attack();
            print("Attack");
            OnAttack?.Invoke();
            Invoke(nameof(ReloadAttack), _attackDelay);
        }
    }

    private void ReloadAttack()
    {
        _canAttack = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position, _attackRange);
    }

}
