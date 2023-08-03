using UnityEngine;
using System;

public class Enemy_Stats : MonoBehaviour
{
    [SerializeField] private Enemy_Info _enemyInfo;
    [SerializeField] private Enemy_Controller _enemyController;
    [SerializeField] private int _health;
    [SerializeField] private int _maxGold;
    [SerializeField] private int _XP;


    public static Action<int, int> onDeath;

    private void Start()
    {
        _enemyController.MinDistanceToPlayer = _enemyInfo.MinDistanceToPlayer;
        _enemyController.Speed = _enemyInfo.Speed;
        _enemyController.AttackSpeed = _enemyInfo.AttackSpeed;
        _health = _enemyInfo.Health;
        _maxGold = _enemyInfo.MaxGold;
        _XP = _enemyInfo.XP;
    }

    private void OnMouseDown()
    {
        TakeDamage(1);
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            onDeath?.Invoke(_maxGold, _XP);
            _enemyController.Death();
        }
    }
}
