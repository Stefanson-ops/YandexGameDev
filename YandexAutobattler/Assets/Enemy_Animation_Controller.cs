using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Animation_Controller : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        Initialize();
    }

    public void Attack()
    {
        _anim.SetTrigger("Attack");
    }

    public void SetRunningState(bool isRunning)
    {
        _anim.SetBool("IsRunning", isRunning);
    }

    public void FlipSprite(bool isFlip)
    {
        _spriteRenderer.flipX = isFlip;
    }

    private void Initialize()
    {
        _anim = GetComponentInChildren<Animator>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
}