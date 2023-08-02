using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy_Info", menuName = "Gameplay/New Enemy")]
public class Enemy_Info : ScriptableObject
{
    [SerializeField] public Sprite _sprite;
    [SerializeField] public int _health;
    [SerializeField] public float _speed;
    [SerializeField] public float _minDistanceToPlayer;
}
