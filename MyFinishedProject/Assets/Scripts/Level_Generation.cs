using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_Generation : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _brick;
    [SerializeField] private GameObject _previousBrick;
    [SerializeField] private Vector3 _brickSpawnOffset;

    [Range(1, 3)]
    [SerializeField] private float _maxGenerationTime;

    [Range(0,3)]
    [SerializeField] private float _maxSpawnDistance;



    private void Awake()
    {
        Initialize();
        Invoke(nameof(GenerateAnotherBrick), 1);
    }

    private void Initialize()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void GenerateAnotherBrick()
    {
        StartCoroutine(BrickGeneration());
    }

    private IEnumerator BrickGeneration()
    {
        float timer = Time.time + Random.Range(.5f, _maxGenerationTime);

        while (Time.time < timer)
        {
            yield return null;
        }

        if (_previousBrick != null)
        {
            Vector3 brickSpawnPosition = _previousBrick.transform.position + _brickSpawnOffset + new Vector3(0, Random.Range(0, _maxSpawnDistance), 0);
            _previousBrick = Instantiate(_brick, brickSpawnPosition, Quaternion.identity);
        }
        else
            _previousBrick = Instantiate(_brick, _brickSpawnOffset, Quaternion.identity);
    }

    private void CheckPlayerPosition()
    {
        if (_previousBrick != null)
            if (_player.transform.position.y < _previousBrick.transform.position.y)
                _player.GetComponent<Player_Controller>().Death();
    }

    public void ClearLevel()
    {
        _previousBrick = null;
        GameObject[] bricks = GameObject.FindGameObjectsWithTag("Bricks");
        foreach (GameObject brick in bricks)
        {
            Destroy(brick);
        }
    }

    public void StartGenerating()
    {
        Invoke(nameof(GenerateAnotherBrick), 1);
    }

    private void OnEnable()
    {
        Brick_Controller.OnClosed += GenerateAnotherBrick;
    }

    private void OnDisable()
    {
        Brick_Controller.OnClosed -= GenerateAnotherBrick;
    }
}
