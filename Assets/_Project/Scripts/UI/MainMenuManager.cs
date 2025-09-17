using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _optionsMenu;
    [SerializeField] private GameObject _shopMenu;
    [SerializeField] private GameObject _leaderboardMenu;

    [Header("Riferimenti Audio Opzioni")]
    [SerializeField] private Slider _masterVolumeSlider;
    [SerializeField] private Slider _musicVolumeSlider;
    [SerializeField] private Slider _sfxVolumeSlider;

    private void Start()
    {
        ShowMainMenu();

        if (OptionsManager.Instance != null)
        {
            if (_masterVolumeSlider) _masterVolumeSlider.value = OptionsManager.Instance.GetMasterVolume();
            if (_musicVolumeSlider) _musicVolumeSlider.value = OptionsManager.Instance.GetMusicVolume();
            if (_sfxVolumeSlider) _sfxVolumeSlider.value = OptionsManager.Instance.GetSFXVolume();
        }
        else
        {
            Debug.LogWarning("OptionsManager non trovato!");
        }
    }

    public void ShowMainMenu()
    {
        _mainMenu.SetActive(true);
        _optionsMenu.SetActive(false);
        _shopMenu.SetActive(false);
        _leaderboardMenu.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneNames.Game);
    }

    public void ShowOptions()
    {
        _mainMenu.SetActive(false);
        _optionsMenu.SetActive(true);
    }

    public void CloseOptions()
    {
        _mainMenu.SetActive(true);
        _optionsMenu.SetActive(false);
    }

    public void ShowShop()
    {
        _mainMenu.SetActive(false);
        _shopMenu.SetActive(true);
    }

    public void CloseShop()
    {
        _mainMenu.SetActive(true);
        _shopMenu.SetActive(false);
    }

    public void ShowLeaderboard()
    {
        _mainMenu.SetActive(false);
        _leaderboardMenu.SetActive(true);
    }

    public void CloseLeaderboard()
    {
        _mainMenu.SetActive(true);
        _leaderboardMenu.SetActive(false);
    }

    public void BackToMainMenu()
    {
        ShowMainMenu();
    }

    public void ExitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
