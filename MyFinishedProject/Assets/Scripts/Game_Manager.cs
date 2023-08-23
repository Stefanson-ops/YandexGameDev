using UnityEngine;
using TMPro;
using YG;

public enum GameStatus
{
    mainMenu,
    gameplay,
    pause,
    deathScreen
}

public class Game_Manager : MonoBehaviour
{
    [Header("AD Controller")]
    [SerializeField] private int _countToShowAD = 0;
    [SerializeField] private int _currentDeathCount = 0;

    [Header("UI Elements")]
    [SerializeField] private GameObject _pCCanvas;
    [SerializeField] private GameObject _mobileCanvas;

    [Space]

    [SerializeField] private GameObject _deathScreen;
    [SerializeField] private GameObject _showContinueButton;
    [SerializeField] private GameObject _pauseMenuUI;
    [SerializeField] private GameObject _settingsMenu;
    [SerializeField] private GameObject _score;
    [SerializeField] private GameObject _scoreInfo;
    [SerializeField] private GameObject _pauseButton;

    [Header("Mobile UI Elements")]
    [SerializeField] private GameObject _mobileDeathScreen;
    [SerializeField] private GameObject _mobileShowContinueButton;
    [SerializeField] private GameObject _mobilePauseMenuUI;
    [SerializeField] private GameObject _mobileSettingsMenu;
    [SerializeField] private GameObject _mobileScore;
    [SerializeField] private GameObject _mobileScoreInfo;
    [SerializeField] private GameObject _mobilePauseButton;


    [Header("PC UI Elements")]
    [SerializeField] private GameObject _pCDeathScreen;
    [SerializeField] private GameObject _pCShowContinueButton;
    [SerializeField] private GameObject _pCPauseMenuUI;
    [SerializeField] private GameObject _pCSettingsMenu;
    [SerializeField] private GameObject _pCScore;
    [SerializeField] private GameObject _pCScoreInfo;
    [SerializeField] private GameObject _pCPauseButton;


    [Header("Game Status")]
    public GameStatus gameStatus;

    [Header("Game Objects")]
    [SerializeField] private GameObject _player;

    [Header("Scripts")]
    [SerializeField] private Level_Generation _levelGeneration;
    [SerializeField] private Music_Controller _musicController;

    [Header("Bools")]
    [SerializeField] private bool _isMobile = true;
    [SerializeField] private bool _gameIsPaused = false;
    [SerializeField] private bool _canStartGame = true;
    [SerializeField] private bool _canContinueGame = false;
    [SerializeField] private bool _playerWatchedTheAd = false;

    [Header("Score")]
    [SerializeField] private int _currentScore;
    [SerializeField] private int _maxScore;
    [SerializeField] private TextMeshProUGUI _maxScoreText;
    [SerializeField] private TextMeshProUGUI _pCMaxScoreText;
    [SerializeField] private TextMeshProUGUI _mobileMaxScoreText;
    [SerializeField] private Animator _scoreAnimator;
    [SerializeField] private TextMeshProUGUI _scoreText;

    private void Awake()
    {
        Initialize();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
            _startGame(Vector2.zero);
    }

    private void Initialize()
    {

        _isMobile = Progress_Manager._isMobile;

        _player = GameObject.FindGameObjectWithTag("Player");
        _levelGeneration = GetComponent<Level_Generation>();
        _musicController = GetComponent<Music_Controller>();

        if (_isMobile)
        {
            _mobileCanvas.SetActive(true);

            _scoreText = _mobileScoreInfo.GetComponent<TextMeshProUGUI>();
            _scoreAnimator = _mobileScoreInfo.GetComponent<Animator>();

            _deathScreen = _mobileDeathScreen;
            _showContinueButton = _mobileShowContinueButton;
            _pauseMenuUI = _mobilePauseMenuUI;
            _settingsMenu = _mobileSettingsMenu;
            _score = _mobileScore;
            _maxScoreText = _mobileMaxScoreText;
            _pauseButton = _mobilePauseButton;
        }
        else
        {
            _pCCanvas.SetActive(true);

            _scoreText = _pCScoreInfo.GetComponent<TextMeshProUGUI>();
            _scoreAnimator = _pCScoreInfo.GetComponent<Animator>();

            _deathScreen = _pCDeathScreen;
            _showContinueButton = _pCShowContinueButton;
            _pauseMenuUI = _pCPauseMenuUI;
            _settingsMenu = _pCSettingsMenu;
            _score = _pCScore;
            _maxScoreText = _pCMaxScoreText;
            _pauseButton = _pCPauseButton;
        }

        ResetScore();

        _score.SetActive(true);
    }

    public void StartGame()
    {
        _canStartGame = true;
        _startGame(Vector2.zero);
    }

    private void _startGame(Vector2 zero)
    {
        if (_canStartGame)
        {
            gameStatus = GameStatus.gameplay;

            Time.timeScale = 1;

            _player.transform.position = new Vector3(0, 0, 0);
            _player.transform.rotation = Quaternion.identity;
            _player.GetComponent<Player_Controller>().ActivatePlayer();

            ResetScore();
            RemoveStartInscription();
            _levelGeneration.ClearLevel();
            _levelGeneration.StartGenerating();
            _musicController.StartGame();

            _deathScreen.SetActive(false);
            _score.SetActive(true);
            _pauseButton.SetActive(true);

            _canStartGame = false;
            _canContinueGame = true;
        }
    }

    public void MainMenu()
    {
        gameStatus = GameStatus.mainMenu;

        Time.timeScale = 1;

        _player.transform.position = new Vector3(0, 0, 0);
        _player.transform.rotation = Quaternion.identity;
        _player.GetComponent<Player_Controller>().ActivatePlayer();

        ResetScore();
        _levelGeneration.ClearLevel();

        _deathScreen.SetActive(false);
        _score.SetActive(true);
        _pauseButton.SetActive(false);

        _canStartGame = true;
    }

    public void ShowAdvForRespawn()
    {
        YandexGame.RewVideoShow(0);
        _deathScreen.SetActive(false);
    }

    public void SetAdvStatus(int id)
    {
        if(_canContinueGame)
            _playerWatchedTheAd = true;
        ContinueGame();
    }

    public void ContinueGame()
    {
        if (_playerWatchedTheAd)
        {
            gameStatus = GameStatus.gameplay;

            _player.transform.position = new Vector3(0, 0, 0);
            _player.transform.rotation = Quaternion.identity;

            _deathScreen.SetActive(false);
            _score.SetActive(true);
            _pauseButton.SetActive(true);


            _player.GetComponent<Player_Controller>().ActivatePlayer();
            _musicController.PauseGame();
            _levelGeneration.ClearLevel();
            _levelGeneration.StartGenerating();

            Time.timeScale = 1;

            _canContinueGame = false;
            _playerWatchedTheAd = false;
        }
        else if (!_canContinueGame)
            print("Nothing");
        else
            StartGame();
    }

    public void EndGame()
    {
        gameStatus = GameStatus.deathScreen;

        Time.timeScale = 0;

        _currentDeathCount++;

        _musicController.PauseGame();

        _deathScreen.SetActive(true);
        _score.SetActive(false);
        _pauseButton.SetActive(false);

        _showContinueButton.SetActive(_canContinueGame);

        _maxScoreText.text = Progress_Manager.Instance.playerInfo.maxScore.ToString();

        SaveData();


        if (_currentDeathCount > _countToShowAD && !_canContinueGame)
        {
            YandexGame.RewVideoShow(0);
            _currentDeathCount = 0;
        }
    }

    public void PauseGame()
    {
        if (gameStatus != GameStatus.deathScreen)
        {
            _gameIsPaused = !_gameIsPaused;

            if (_gameIsPaused)
            {
                Time.timeScale = 0;
                gameStatus = GameStatus.pause;
                _musicController.PauseGame();
                _pauseMenuUI.SetActive(true);
                _score.SetActive(false);
                _pauseButton.SetActive(false);

            }
            else
            {
                Time.timeScale = 1;
                gameStatus = GameStatus.gameplay;
                _musicController.PauseGame();
                _pauseMenuUI.SetActive(false);
                _score.SetActive(true);
                _pauseButton.SetActive(true);

            }
        }
    }

    public void ShowSettingsMenu()
    {
        _settingsMenu.SetActive(true);
        _pauseMenuUI.SetActive(false);
    }

    public void HideSettingsMenu()
    {
        _settingsMenu.SetActive(false);
        PauseGame();
    }

    private void AddScore()
    {
        _currentScore++;
        if (Progress_Manager.Instance.playerInfo.maxScore < _currentScore)
            Progress_Manager.Instance.playerInfo.maxScore = _currentScore;
        _scoreAnimator.SetTrigger("Update");
        _scoreText.text = _currentScore.ToString();
    }

    private void SaveData()
    {
        if (_currentScore >= Progress_Manager.Instance.playerInfo.maxScore)
        {
            Progress_Manager.Instance.Save();
        }
    }

    private void ResetScore()
    {
        _currentScore = 0;
        if (!_isMobile)
        {
            if (Language.Instance.currentLanguage == "en")
                _scoreText.text = "Press Space";
            else if (Language.Instance.currentLanguage == "ru")
                _scoreText.text = "Нажми пробел";
            else
                _scoreText.text = "Press Space";
        }
        else
        {
            if(Language.Instance.currentLanguage=="en")
                _scoreText.text = "Swipe up";
            else if(Language.Instance.currentLanguage == "ru")
                _scoreText.text = "Свайп вверх";
            else
                _scoreText.text = "Swipe up";
        }
    }

    private void RemoveStartInscription()
    {
        _scoreText.text = _currentScore.ToString();
    }

    private void OnEnable()
    {
        Player_Controller.onDeath += EndGame;
        Brick_Controller.OnClosed += AddScore;
        Input_Controller.swipeEvent += _startGame;
        YandexGame.RewardVideoEvent += SetAdvStatus;
    }

    private void OnDisable()
    {
        Player_Controller.onDeath -= EndGame;
        Brick_Controller.OnClosed -= AddScore;
        Input_Controller.swipeEvent -= _startGame;
        YandexGame.RewardVideoEvent -= SetAdvStatus;

    }

}
