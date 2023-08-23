using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Sound_Controller : MonoBehaviour
{
    [SerializeField] private AudioClip _jumpSound;
    [SerializeField] private AudioClip _deathSound;
    [SerializeField] private AudioSource _audioSource;
    [Range(1, 2)]
    [SerializeField] private float _maxPitch;


    public void PlayDeathSound()
    {
        _audioSource.pitch = Random.Range(1, _maxPitch);
        _audioSource.PlayOneShot(_deathSound);
    }

    public void PlayerJumpSound()
    {
        _audioSource.pitch = 1;
        _audioSource.PlayOneShot(_jumpSound);
    }
}
