using System.Collections;
using UnityEngine;

public class Level_Generation : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject _brickPrefab;
    [SerializeField] private GameObject[] _building;

    [Header("Links")]
    [SerializeField] private GameObject _player;
    [SerializeField] private Vector3 _previousBrickPosition;

    [Header("Spawn parameters")]
    [SerializeField] private Vector3 _brickSpawnOffset;

    [Range(1, 3)]
    [SerializeField] private float _maxBrickGenerationTime;
    [Range(0, 2)]
    [SerializeField] private float _maxBuildingGenerationTime;

    [Range(0,3)]
    [SerializeField] private float _maxBrickSpawnDistance;
    [SerializeField] private float _maxBuildingSpawnDistance;



    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void GenerateAnotherBrick()
    {
        StartCoroutine(BrickGeneration());
        CheckPlayerPosition();
    }

    private IEnumerator BrickGeneration()
    {
        float timer = Time.time + Random.Range(.75f, _maxBrickGenerationTime);

        Vector3 brick;

        while (Time.time < timer)
        {
            yield return null;
        }

        if (_previousBrickPosition != Vector3.zero)
        {
            Vector3 brickSpawnPosition = _previousBrickPosition + _brickSpawnOffset + new Vector3(0, Random.Range(0, _maxBrickSpawnDistance), 0);
            brick = Instantiate(_brickPrefab, brickSpawnPosition, Quaternion.identity).transform.position;
            _previousBrickPosition = brick;
        }
        else
        {
            brick = Instantiate(_brickPrefab, _brickSpawnOffset, Quaternion.identity).transform.position;
            _previousBrickPosition = brick;
        }
    }

    private IEnumerator SpawnBuildings()
    {
        float timer = Time.time + Random.Range(0, _maxBuildingGenerationTime);

        while (Time.time < timer)
        {
            yield return null;
        }

        Vector3 defPosition = Vector3.zero;

        int coin = Random.Range(0, 2);

        if (coin == 0)
            defPosition.x = Random.Range(-_maxBuildingSpawnDistance, -8);
        else
            defPosition.x = Random.Range(8, _maxBuildingSpawnDistance);

        print(coin);

        Instantiate(_building[0], new Vector3(defPosition.x, _player.transform.position.y + Random.Range(0, -60), Random.Range(130, 150)), Quaternion.Euler(Random.Range(-2, 2), 0, Random.Range(-2, 2)));

        StartCoroutine(SpawnBuildings());
    }

    private void CheckPlayerPosition()
    {
        if (_previousBrickPosition != Vector3.zero)
            if (_player.transform.position.y < _previousBrickPosition.y)
                _player.GetComponent<Player_Controller>().Death("Death From Position");
    }

    public void ClearLevel()
    {
        StopAllCoroutines();
        _previousBrickPosition = Vector3.zero;
        GameObject[] bricks = GameObject.FindGameObjectsWithTag("Bricks");
        foreach (GameObject brick in bricks)
        {
            Destroy(brick);
        }
    }

    public void StartGenerating()
    {
        Invoke(nameof(GenerateAnotherBrick), 1);
        StartCoroutine(SpawnBuildings());
    }

    private void OnEnable()
    {
        Brick_Controller.OnClosed += GenerateAnotherBrick;
    }

    private void OnDisable()
    {
        Brick_Controller.OnClosed += GenerateAnotherBrick;
    }
}
