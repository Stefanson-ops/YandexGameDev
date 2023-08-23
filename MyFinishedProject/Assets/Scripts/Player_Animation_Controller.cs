using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Animation_Controller : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    [SerializeField] private GameObject _body;
    [SerializeField] private ParticleSystem _particles;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _anim = GetComponent<Animator>();
    }

    public void AnimateJump()
    {
        _anim.SetTrigger("Jump");
    }

    public void AnimateDeath()
    {
        _body.SetActive(false);
        _particles.Play();
    }

    public void ResetPlayer()
    {
        _body.SetActive(true);
        _particles.Stop();
    }
}
