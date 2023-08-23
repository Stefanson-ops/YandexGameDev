using UnityEngine;
using YG;

[System.Serializable]
public class PlayerInfo
{
    public int maxScore;
}

public class Progress_Manager : MonoBehaviour
{
    public PlayerInfo playerInfo;

    public static bool _isMobile = false;

    public static Progress_Manager Instance;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        if (Instance == null)
        {
            transform.parent = null;
            Instance = this;
            CheckTypeOfDevice();
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void StartAuthorization()
    {

    }

    public void StartLoadingGame()
    {
        Debug.Log("Progress Manager start game");
    }

    public void Save()
    {
        if (YandexGame.auth)
        {
            YandexGame.savesData.maxScore = playerInfo.maxScore;
            YandexGame.NewLeaderboardScores("Leaderboard", playerInfo.maxScore);
            YandexGame.SaveProgress();
        }
    }

    public void LoadPlayerInfo()
    {
        if(YandexGame.auth)
            playerInfo.maxScore = YandexGame.savesData.maxScore;
    }

    private void CheckTypeOfDevice()
    {
        _isMobile = Application.isMobilePlatform;
    }

    private void OnEnable()
    {
        YandexGame.GetDataEvent += LoadPlayerInfo;
    }

    private void OnDisable()
    {
        YandexGame.GetDataEvent -= LoadPlayerInfo;
    }
}
