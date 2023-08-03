using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy_Info", menuName = "Gameplay/New Enemy")]
public class Enemy_Info : ScriptableObject
{
    [SerializeField] public Sprite Sprite;
    [SerializeField] public int Health;
    [SerializeField] public float Speed;
    [SerializeField] public float MinDistanceToPlayer;
    [SerializeField] public int MaxGold;
    [SerializeField] public int XP;
    [SerializeField] public float AttackSpeed;
}
