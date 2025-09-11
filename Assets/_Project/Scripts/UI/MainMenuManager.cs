using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _optionsMenu;
    [SerializeField] private GameObject _shopMenu;
    [SerializeField] private GameObject _leaderboardMenu;

    private void Start()
    {
        ShowMainMenu();
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
        SceneManager.LoadScene("GameScene"); 
    }

    public void ShowOptions()
    {
        _mainMenu.SetActive(false);
        _optionsMenu.SetActive(true);
    }

    public void ShowShop()
    {
        _mainMenu.SetActive(false);
        _shopMenu.SetActive(true);
    }

    public void ShowLeaderboard()
    {
        _mainMenu.SetActive(false);
        _leaderboardMenu.SetActive(true);
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
