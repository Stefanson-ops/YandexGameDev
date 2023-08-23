using UnityEngine;
using YG;

public class Language : MonoBehaviour
{
    public string currentLanguage;


    public static Language Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            currentLanguage = YandexGame.EnvironmentData.language;
            print(YandexGame.EnvironmentData.language);
        }
        else
            Destroy(gameObject);
    }
}
