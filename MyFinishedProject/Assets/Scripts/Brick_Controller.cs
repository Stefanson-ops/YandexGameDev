using UnityEngine;
using System;

public class Brick_Controller : MonoBehaviour
{
    public static Action OnClosed;

    private void Start()
    {
        Destroy(gameObject, 8);
    }


    public void Closed()
    {
        OnClosed?.Invoke();
    }
}
