using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { Start, InGame, Paused, GameOver }

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private GameState _currentState = GameState.Start;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        //SetState(GameState.Start);
        SetState(GameState.InGame); 
    }

    public void SetState(GameState newState)
    {
        _currentState = newState;

        switch (newState)
        {
            case GameState.Start:
                Time.timeScale = 0f;
                break;

            case GameState.InGame:
                Time.timeScale = 1f;
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

    public void StartGame() => SetState(GameState.InGame);
    public void PauseGame() => SetState(GameState.Paused);
    public void ResumeGame() => SetState(GameState.InGame);
    public void EndGame() => SetState(GameState.GameOver);

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneNames.MainMenu);
    }

    public void ExitGame() => Application.Quit();
}
