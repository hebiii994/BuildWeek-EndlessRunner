using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private TMP_Text _scoreText;           
    [SerializeField] private TMP_Text _trophyCounterText;   
    [SerializeField] private Image _trophyIcon;
    [SerializeField] private Image[] _powerupSlots;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _gameOverMenu;
    [SerializeField] private GameObject _optionsMenu;

    private int _score = 0;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        UpdateScore(0);
        UpdateTrophyUI(TrophyManager.Instance.GetTrophies());

        TrophyManager.Instance.OnTrophyChanged += UpdateTrophyUI;
    }

    private void OnDestroy()
    {
        if (TrophyManager.Instance != null)
            TrophyManager.Instance.OnTrophyChanged -= UpdateTrophyUI;
    }

    public void UpdateScore(int newScore)
    {
        _score = newScore;
        _scoreText.text = _score.ToString();
    }

    public void AddScore(int amount)
    {
        _score += amount;
        UpdateScore(_score);
    }

    private void UpdateTrophyUI(int total)
    {
        _trophyCounterText.text = total.ToString();
    }

    public void SetCollectibleIcon(Sprite icon)
    {
        _trophyIcon.sprite = icon;
        _trophyIcon.enabled = true;
    }

    public void SetPowerup(int slotIndex, Sprite icon)
    {
        if (slotIndex >= 0 && slotIndex < _powerupSlots.Length)
        {
            _powerupSlots[slotIndex].sprite = icon;
            _powerupSlots[slotIndex].enabled = true;
        }
    }

    public void ClearPowerup(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < _powerupSlots.Length)
        {
            _powerupSlots[slotIndex].enabled = false;
        }
    }

    public void ShowPauseMenu(bool show) => _pauseMenu.SetActive(show);
    public void ShowGameOver(bool show) => _gameOverMenu.SetActive(show);
    public void ShowOptions(bool show) => _optionsMenu.SetActive(show);
}
