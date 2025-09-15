using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuUI : MonoBehaviour
{
    [SerializeField] private Slider _masterSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider;

    private void OnEnable()
    {
        if (OptionsManager.Instance != null)
        {
            _masterSlider.value = OptionsManager.Instance.GetMasterVolume();
            _musicSlider.value = OptionsManager.Instance.GetMusicVolume();
            _sfxSlider.value = OptionsManager.Instance.GetSFXVolume();
        }
    }

    public void OnMasterChanged(float value) => OptionsManager.Instance.SetMasterVolume(value);
    public void OnMusicChanged(float value) => OptionsManager.Instance.SetMusicVolume(value);
    public void OnSFXChanged(float value) => OptionsManager.Instance.SetSFXVolume(value);
}
