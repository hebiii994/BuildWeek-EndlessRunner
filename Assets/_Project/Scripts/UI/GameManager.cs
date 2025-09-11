using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { Start, InGame, Paused, GameOver }

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState currentState = GameState.Start;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        SetState(GameState.Start);
    }

    public void SetState(GameState newState)
    {
        currentState = newState;

        switch (newState)
        {
            case GameState.Start:
                Time.timeScale = 0f;
                UIManager.Instance.ShowMainMenu(true);
                break;

            case GameState.InGame:
                Time.timeScale = 1f;
                UIManager.Instance.ShowMainMenu(false);
                UIManager.Instance.ShowPauseMenu(false);
                UIManager.Instance.ShowGameOver(false);
                break;

            case GameState.Paused:
                Time.timeScale = 0f;
                UIManager.Instance.ShowPauseMenu(true);
                break;

            case GameState.GameOver:
                Time.timeScale = 0f;
                UIManager.Instance.ShowGameOver(true);
                break;
        }
    }

    public void StartGame()
    {
        SetState(GameState.InGame);
    }

    public void PauseGame()
    {
        SetState(GameState.Paused);
    }

    public void ResumeGame()
    {
        SetState(GameState.InGame);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); 
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void EndGame()
    {
        SetState(GameState.GameOver);
    }
}
