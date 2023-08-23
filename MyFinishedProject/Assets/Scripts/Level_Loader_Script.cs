using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class Level_Loader_Script : MonoBehaviour
{
    [SerializeField] private float _introDelay;
    [SerializeField] private float _transitionTime;

    [SerializeField] private bool _isStartScreen = false;

    [SerializeField] private Animator _transition;

    [SerializeField] private bool _isMobile = true;

    [SerializeField] private GameObject _mobileAuth;
    [SerializeField] private GameObject _pCAuth;
    [SerializeField] private GameObject _mobileLoader;
    [SerializeField] private GameObject _pCLoader;
    private bool _gameIsStarted = false;

    private void Awake()
    {
        _isMobile = Progress_Manager._isMobile;
    }

    private void Auth()
    {
        if (_isStartScreen && !_gameIsStarted)
        {
            if (!YandexGame.auth)
            {
                if (_isMobile)
                    _mobileAuth.SetActive(true);
                else
                    _pCAuth.SetActive(true);
            }
            else
                StartGame();
        }
    }

    public void StartGame()
    {
        _gameIsStarted = true;
        Debug.Log("Level Loader start game");

        _pCAuth.SetActive(false);
        _mobileAuth.SetActive(false);

        if (_isStartScreen)
            Invoke(nameof(LoadNextLevel), _introDelay);
        else
        {
            Destroy(gameObject, 2);
            return;
        }

        if (_isMobile)
            _mobileLoader.SetActive(true);
        else
            _pCLoader.SetActive(true);
    }

    private void LoadNextLevel()
    {
        _transition.SetTrigger("Start");
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        yield return new WaitForSeconds(_transitionTime);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OnEnable()
    {
        YandexGame.GetDataEvent += Auth;
    }

    private void OnDisable()
    {
        YandexGame.GetDataEvent -= Auth;
    }
}
