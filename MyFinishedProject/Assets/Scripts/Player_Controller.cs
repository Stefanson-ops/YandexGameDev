using UnityEngine;
using System;

public class Player_Controller : MonoBehaviour
{
    public static Action onDeath;

    [Header("Scripts")]
    [SerializeField] private Player_Animation_Controller _playerAnimController;
    [SerializeField] private Player_Sound_Controller _playerSoundController;
    [SerializeField] private Camera_Controller _cameraController;


    [Header("Components")]
    [SerializeField] private GameObject _player;
    [SerializeField] private Rigidbody _rb;

    [Header("Values")]
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private float _minDistanceToFloor = 1f;
    [SerializeField] private float _extraGravity = 8f;

    [Header("Bools")]
    [SerializeField] private bool _onFloor = false;
    [SerializeField] private bool _isDead = false;

    private void Awake()
    {
        Initialize();
    }

    private void Update()
    {
        if (!_isDead)
            CheckForFloor();
    }

    private void FixedUpdate()
    {
        if (!_isDead)
            AddExtraForces();
        else
            return;
    }

    private void Initialize()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _rb = _player.GetComponent<Rigidbody>();
        _playerAnimController = GetComponent<Player_Animation_Controller>();
        _playerSoundController = GetComponent<Player_Sound_Controller>();
    }

    public void Jump(Vector2 forceDirection)
    {
        Vector3 jumpDirection = new Vector3(0, forceDirection.y, 0);

        if (jumpDirection.y < 0)
            _rb.AddForce(jumpDirection * _jumpForce * 2, ForceMode.Impulse);

        if (_onFloor && !_isDead)
        {
            _playerSoundController.PlayerJumpSound();

            _rb.AddForce(jumpDirection * _jumpForce, ForceMode.Impulse);

            Vector3 randomTorque = new Vector3(UnityEngine.Random.Range(-1, 1), UnityEngine.Random.Range(-1, 1), UnityEngine.Random.Range(-1, 1));
            _rb.AddTorque(randomTorque * _jumpForce, ForceMode.Impulse);

            _playerAnimController.AnimateJump();

            print("Jump");
        }
    }

    private void Attack()
    {
        print("Attack");
    }

    private void CheckForFloor()
    {
        _onFloor = Physics.Raycast(_player.transform.position, Vector3.down, _minDistanceToFloor);
    }

    private void AddExtraForces()
    {
        _rb.AddForce(Vector3.down * _extraGravity, ForceMode.Force);
    }

    public void Death(string reason)
    {
        if (!_isDead)
        {
            _isDead = true;
            _rb.isKinematic = true;
            _playerAnimController.AnimateDeath();
            _playerSoundController.PlayDeathSound();
            _cameraController.StartCameraShake();
            Invoke(nameof(NotifyOtherScripts), 2f);
            print(reason);
        }
    }

    private void NotifyOtherScripts()
    {
        onDeath?.Invoke();
    }

    public void ActivatePlayer()
    {
        _isDead = false;
        _rb.isKinematic = false;
        _playerAnimController.ResetPlayer();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dead Zone"))
            Death("Death From Dead Zone");
    }

    private void OnEnable()
    {
        Input_Controller.attackEvent += Attack;
        Input_Controller.swipeEvent += Jump;
    }

    private void OnDisable()
    {
        Input_Controller.attackEvent -= Attack;
        Input_Controller.swipeEvent -= Jump;
    }
}
