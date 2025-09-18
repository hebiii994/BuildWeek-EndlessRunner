using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Riferimenti Audio")]
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _sfxSource;

    [SerializeField] private AudioClip _menuMusic;
    [SerializeField] private AudioClip _gameMusic;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        if (scene.name == SceneNames.MainMenu) 
        {
            PlayMusic(_menuMusic);
        }
        else if (scene.name == SceneNames.Game) 
        {
            PlayMusic(_gameMusic);
        }
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        if (clip != null && _musicSource.clip != clip)
        {
            _musicSource.clip = clip;
            _musicSource.loop = true;
            _musicSource.Play();
        }
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            _sfxSource.PlayOneShot(clip);
        }
    }
}
