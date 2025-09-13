using UnityEngine;
using UnityEngine.Audio;

public class OptionsManager : MonoBehaviour
{
    public static OptionsManager Instance;

    [SerializeField] private AudioMixer _audioMixer;
    private float _masterVolume = 1f;
    private float _musicVolume = 1f;
    private float _sfxVolume = 1f;

    private void Awake()
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

    public void SetMasterVolume(float value)
    {

        _masterVolume = Mathf.Clamp(value, 0.0001f, 1f);
        _audioMixer.SetFloat("MasterVolume", Mathf.Log10(_masterVolume) * 20);
    }
    public float GetMasterVolume() => _masterVolume;

    public void SetMusicVolume(float value)
    {
        _musicVolume = Mathf.Clamp(value, 0.0001f, 1f);
        _audioMixer.SetFloat("MusicVolume", Mathf.Log10(_musicVolume) * 20);
    }
    public float GetMusicVolume() => _musicVolume;

    public void SetSFXVolume(float value)
    {
        _sfxVolume = Mathf.Clamp(value, 0.0001f, 1f);
        _audioMixer.SetFloat("SFXVolume", Mathf.Log10(_sfxVolume) * 20);
    }
    public float GetSFXVolume() => _sfxVolume;
}
