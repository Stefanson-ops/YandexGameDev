using UnityEngine;
using TMPro;

public class International_Text : MonoBehaviour
{

    [SerializeField] private string _en;
    [SerializeField] private string _ru;

    private void Start()
    {
        print(Language.Instance.currentLanguage);
        if (Language.Instance.currentLanguage == "en")
        {
            GetComponent<TextMeshProUGUI>().text = _en;
        }
        else if (Language.Instance.currentLanguage == "ru")
        {
            GetComponent<TextMeshProUGUI>().text = _ru;
        }
        else
        {
            GetComponent<TextMeshProUGUI>().text = _en;
        }
    }
}
