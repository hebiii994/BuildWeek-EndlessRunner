using UnityEngine;
using UnityEngine.Audio;

public class OptionsManager : MonoBehaviour
{
    public static OptionsManager Instance;

    [SerializeField] private AudioMixer audioMixer;
    private float volume = 1f;

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
        volume = value;
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
    }

    public float GetVolume()
    {
        return volume;
    }
}
