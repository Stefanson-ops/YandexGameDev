using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    [Header("Camera Shake Parameters")]
    [SerializeField] private float _cameraShakeDuration;
    [SerializeField] private float _cameraShakeMagnitude;
    [SerializeField] private Transform _camera;

    [Header("Camera Parameters")]
    [SerializeField] private float _cameraFollowSpeed;
    [SerializeField] private Transform _targetToFollow;
    [SerializeField] private Vector3 _followOffset;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        _targetToFollow = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        FollowToPlayer();
    }

    private void FollowToPlayer()
    {
        if(_targetToFollow!=null)
            transform.position = Vector3.Slerp(transform.position, _targetToFollow.position + _followOffset, _cameraFollowSpeed * Time.deltaTime);
        else return;
    }

    public void StartCameraShake()
    {
        StartCoroutine(Shake(_cameraShakeDuration, _cameraShakeMagnitude));
    }

    private IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = _camera.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            Vector3 newPos = new Vector3(x, y, originalPos.z);

            _camera.localPosition = newPos;

            elapsed += Time.deltaTime;

            yield return null;
        }

        _camera.localPosition = originalPos;
    }
}
