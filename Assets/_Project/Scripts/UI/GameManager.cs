using UnityEngine;

public enum GameState { Start, InGame, GameOver }

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
                break;

            case GameState.InGame:
                Time.timeScale = 1f;
                break;

            case GameState.GameOver:
                Time.timeScale = 0f;
                UIManager.Instance.ShowGameOver();
                break;
        }
    }

    public void StartGame()
    {
        SetState(GameState.InGame);
    }

    public void EndGame()
    {
        SetState(GameState.GameOver);
    }
}
