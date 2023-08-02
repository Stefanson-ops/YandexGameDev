using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawner : MonoBehaviour
{
    public int CurrentEnemiesCount = 1;
    [SerializeField] private GameObject[] Enemies;

    public void SpawnEnemies()
    {
        for (int i = 0; i < CurrentEnemiesCount; i++)
        {
            GameObject newEnemy = Instantiate(Enemies[Random.Range(0, Enemies.Length)], new Vector3(Random.Range(-8f, 8f), Random.Range(-4f, 4f), 0), Quaternion.identity);
        }
    }
}
