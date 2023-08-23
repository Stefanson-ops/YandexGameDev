using UnityEngine;
using System;

public class Input_Controller : MonoBehaviour
{
    public static Action<Vector2> swipeEvent;
    public static Action attackEvent;

    [Header("PC Input")]
    [SerializeField] private KeyCode _jumpButton;

    [Header("Mobile Input")]
    [SerializeField] private float _deadZoneDistance = 80f;
    [SerializeField] private Vector2 _tapPosition;
    [SerializeField] private Vector2 _swipeDelta;

    [Header("Bools")]
    [SerializeField] private bool _isMobile;
    [SerializeField] private bool _isSwiping;

    private void Awake()
    {
        Initialize();
    }

    private void Update()
    {
        MyInput();
    }

    private void Initialize()
    {
        _isMobile = Progress_Manager._isMobile;
    }

    private void MyInput()
    {
        if (!_isMobile)
        {
            //”правление на пк
            if (Input.GetKeyDown(_jumpButton))
                swipeEvent?.Invoke(Vector2.up);
        }
        else
        {
            //”правление на мобилке
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    _isSwiping = true;
                    _tapPosition = Input.GetTouch(0).position;
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Canceled || Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    CheckSwipe();
                }
            }
        }
    }

    private void CheckSwipe()
    {
        _swipeDelta = Vector2.zero;

        if (_isSwiping && Input.touchCount > 0)
        {
            _swipeDelta = Input.GetTouch(0).position - _tapPosition;
        }

        if (_swipeDelta.magnitude > _deadZoneDistance)
        {
            if (Mathf.Abs(_swipeDelta.y) > Math.Abs(_swipeDelta.x))
                swipeEvent?.Invoke(_swipeDelta.y > 0 ? Vector2.up : Vector2.down);
        }

        ResetSwipe();
    }

    private void ResetSwipe()
    {
        _isSwiping = false;

        _tapPosition = Vector2.zero;
        _swipeDelta = Vector2.zero;
    }


}
