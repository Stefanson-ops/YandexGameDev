using UnityEngine;

public class Enemy_Stats : MonoBehaviour
{
    [SerializeField] private Enemy_Info _enemyInfo;
    [SerializeField] private Enemy_Controller _enemyController;
    [SerializeField] private int _health;

    private void Start()
    {
        _enemyController._minDistanceToPlayer = _enemyInfo._minDistanceToPlayer;
        _enemyController._speed = _enemyInfo._speed;
        _health = _enemyInfo._health;
        
    }

    private void OnMouseDown()
    {
        TakeDamage(1);
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0)
            Destroy(gameObject);
    }
}
