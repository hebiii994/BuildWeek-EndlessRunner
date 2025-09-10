using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private Image collectibleIcon;
    [SerializeField] private Image[] powerupSlots;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOverMenu;

    private int score = 0;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void UpdateScore(int newScore)
    {
        score = newScore;
        scoreText.text = score.ToString();
    }

    public void SetCollectibleIcon(Sprite icon)
    {
        collectibleIcon.sprite = icon;
        collectibleIcon.enabled = true;
    }

    public void SetPowerup(int slotIndex, Sprite icon)
    {
        if (slotIndex >= 0 && slotIndex < powerupSlots.Length)
        {
            powerupSlots[slotIndex].sprite = icon;
            powerupSlots[slotIndex].enabled = true;
        }
    }

    public void ClearPowerup(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < powerupSlots.Length)
        {
            powerupSlots[slotIndex].enabled = false;
        }
    }

    public void ShowPauseMenu(bool show)
    {
        pauseMenu.SetActive(show);
    }

    public void ShowGameOver()
    {
        gameOverMenu.SetActive(true);
    }
}
