using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private float _magnitude;

    private void Shake()
    {
        StartCoroutine(StartShaking());
    }


    IEnumerator StartShaking()
    {
        Vector3 originalPosition = transform.localPosition;

        float elapsed = 0f;

        while (elapsed < _duration)
        {
            float x = Random.Range(-1f, 1f) * _magnitude;
            float y = Random.Range(-1f, 1f) * _magnitude;

            transform.localPosition = new Vector3(x, y, originalPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPosition;
    }

    private void OnEnable()
    {
        Attack_Controller.OnAttack += Shake;
    }

    private void OnDisable()
    {
        Attack_Controller.OnAttack -= Shake;
            
    }
}
