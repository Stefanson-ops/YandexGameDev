using UnityEngine;

public class Music_Controller : MonoBehaviour
{
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioClip[] _musicClip;
    [SerializeField] private float _musicVolume;
    [SerializeField] private float _maxMusicVolume;
    [SerializeField] private float _musicChangeRate;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _musicSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        _musicSource.volume = Mathf.Lerp(_musicSource.volume, _musicVolume, _musicChangeRate * Time.unscaledDeltaTime);
    }

    public  void StartGame()
    {
        _musicSource.clip = _musicClip[Random.Range(0, _musicClip.Length)];
        _musicVolume = _maxMusicVolume;
        _musicSource.Play();
    }

    public  void StopGame()
    {
        _musicVolume = 0;
    }

    public void PauseGame()
    {
        if(_musicVolume == 0)
            _musicVolume = _maxMusicVolume;
        else
            _musicVolume = 0;
    }
}
