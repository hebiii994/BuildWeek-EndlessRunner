using UnityEngine;
using UnityEngine.Audio;

public class OptionsManager : MonoBehaviour
{
    public static OptionsManager Instance;

    [SerializeField] private AudioMixer _audioMixer;
    private float _volume = 1f;

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

    public void SetVolume(float value)
    {
        _volume = Mathf.Clamp(value, 0.0001f, 1f); // prevenire log10(0)
        _audioMixer.SetFloat("MasterVolume", Mathf.Log10(_volume) * 20);
    }

    public float GetVolume() => _volume;
}
