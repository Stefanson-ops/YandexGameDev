using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStatus
{
    mainMenu,
    gameplay,
    pause,
    deathScreen
}

public class Game_Manager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject _deathScreen;
    [SerializeField] private GameObject _pauseMenuUI;
    public GameStatus gameStatus;

    [Header("Game Objects")]
    [SerializeField] private GameObject _player;

    [Header("Scripts")]
    [SerializeField] private Level_Generation _levelGeneration;
    [SerializeField] private Progress_Manager _progressManager;

    [Header("Bools")]
    [SerializeField] private bool gameIsPaused = false;


    private void Awake()
    {
        Initialize();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            PauseGame();
    }

    private void Initialize()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _levelGeneration = GetComponent<Level_Generation>();
        _progressManager = GetComponent<Progress_Manager>();
    }

    public void StartGame()
    {
        gameStatus = GameStatus.gameplay;

        Time.timeScale = 1;

        _player.transform.position = new Vector3(0, 1, 0);
        _player.GetComponent<Player_Controller>().ActivatePlayer();

        _levelGeneration.ClearLevel();
        _levelGeneration.StartGenerating();
        _deathScreen.SetActive(false);

    }

    public void EndGame()
    {
        gameStatus = GameStatus.deathScreen;

        Time.timeScale = 0;

        _deathScreen.SetActive(true);


    }

    public void PauseGame()
    {
        if (gameStatus != GameStatus.deathScreen)
        {
            gameIsPaused = !gameIsPaused;

            if (gameIsPaused)
            {
                Time.timeScale = 0;
                gameStatus = GameStatus.pause;
                _pauseMenuUI.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                gameStatus = GameStatus.gameplay;
                _pauseMenuUI.SetActive(false);
            }
        }
    }

    private void OnEnable()
    {
        Player_Controller.onDeath += EndGame;
    }

    private void OnDisable()
    {
        Player_Controller.onDeath -= EndGame;
    }

}
