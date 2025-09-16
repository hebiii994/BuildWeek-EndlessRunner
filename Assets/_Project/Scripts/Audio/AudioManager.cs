using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Riferimenti Audio")]
    [SerializeField] private AudioMixer _masterMixer;
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _sfxSource;

    [Header("Nomi dei Parametri del Mixer")]
    [SerializeField] private string _masterVolumeParam = "MasterVolume";
    [SerializeField] private string _musicVolumeParam = "MusicVolume";
    [SerializeField] private string _sfxVolumeParam = "SFXVolume";


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


    public void SetMasterVolume(float volume)
    {

        _masterMixer.SetFloat(_masterVolumeParam, Mathf.Log10(Mathf.Max(volume, 0.0001f)) * 20);
    }


    public void SetMusicVolume(float volume)
    {
        _masterMixer.SetFloat(_musicVolumeParam, Mathf.Log10(Mathf.Max(volume, 0.0001f)) * 20);
    }


    public void SetSFXVolume(float volume)
    {
        _masterMixer.SetFloat(_sfxVolumeParam, Mathf.Log10(Mathf.Max(volume, 0.0001f)) * 20);
    }
}

