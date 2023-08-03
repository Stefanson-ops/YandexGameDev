using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    [SerializeField] private GameObject[] Enemies;
    [SerializeField] private GameObject _player;
    [SerializeField] private Enemy_Spawner _enemySpawner;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        CheckForEnemies(0, 0);
    }

    private void CheckForEnemies(int gold, int xp)
    {
        StartCoroutine(FindEnemies());
    }

    private IEnumerator FindEnemies()
    {
        yield return new WaitForSeconds(0.1f);

        Enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (Enemies.Length > 0)
            print("We have " + Enemies.Length);
        else
        {
            _enemySpawner.SpawnEnemies();
            //_player.GetComponent<Player_Controller>().FindTarget();
        }
    }

    private void OnEnable()
    {
        Enemy_Stats.onDeath += CheckForEnemies;
    }

    private void OnDisable()
    {
        Enemy_Stats.onDeath -= CheckForEnemies;
    }
}
