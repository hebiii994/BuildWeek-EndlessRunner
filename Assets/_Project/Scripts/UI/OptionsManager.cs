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
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
            Load();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Load()
    {
        SaveData data = SaveSystem.Load() ?? new SaveData();

        _masterVolume = data.masterVolume;
        _musicVolume = data.musicVolume;
        _sfxVolume = data.sfxVolume;

        ApplyVolumes();
    }

    private void Save()
    {
        SaveData data = SaveSystem.Load() ?? new SaveData();
        data.masterVolume = _masterVolume;
        data.musicVolume = _musicVolume;
        data.sfxVolume = _sfxVolume;
        SaveSystem.Save(data);
    }

    private void ApplyVolumes()
    {
        _audioMixer.SetFloat("MasterVolume", Mathf.Log10(_masterVolume) * 20);
        _audioMixer.SetFloat("MusicVolume", Mathf.Log10(_musicVolume) * 20);
        _audioMixer.SetFloat("SFXVolume", Mathf.Log10(_sfxVolume) * 20);
    }

    public void SetMasterVolume(float value)
    {
        _masterVolume = Mathf.Clamp(value, 0.0001f, 1f);
        Save();
        ApplyVolumes();
    }
    public float GetMasterVolume() => _masterVolume;

    public void SetMusicVolume(float value)
    {
        _musicVolume = Mathf.Clamp(value, 0.0001f, 1f);
        Save();
        ApplyVolumes();
    }
    public float GetMusicVolume() => _musicVolume;

    public void SetSFXVolume(float value)
    {
        _sfxVolume = Mathf.Clamp(value, 0.0001f, 1f);
        Save();
        ApplyVolumes();
    }
    public float GetSFXVolume() => _sfxVolume;
}
