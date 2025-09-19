using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private AbstractPowerUp _powerUp;
    [SerializeField] private Button _buyButton;
    [SerializeField] private TMP_Text _costText;

    private void Start()
    {
        if (_powerUp != null && _costText != null)
        {
            _costText.text = _powerUp.Cost.ToString();
        }

        if (_buyButton != null)
            _buyButton.onClick.AddListener(BuyPowerUp);

        UpdateButton(TrophyManager.Instance.GetTrophies());
        TrophyManager.Instance.OnTrophyChanged += UpdateButton;
    }

    private void OnDestroy()
    {
        if (TrophyManager.Instance != null)
            TrophyManager.Instance.OnTrophyChanged -= UpdateButton;
    }

    private void UpdateButton(int currentTrophies)
    {
        if (_powerUp == null || _buyButton == null) return;

        _buyButton.interactable = currentTrophies >= _powerUp.Cost &&
                                  !SaveSystem.Load().ownedPowerUps.Contains(_powerUp.PowerUpID);
    }

    private void BuyPowerUp()
    {
        if (_powerUp == null) return;

        TrophyManager trophyManager = TrophyManager.Instance;
        SaveData data = SaveSystem.Load();

        if (trophyManager != null && data != null)
        {
            if (trophyManager.SpendTrophies(_powerUp.Cost))
            {
                if (!data.ownedPowerUps.Contains(_powerUp.PowerUpID))
                {
                    data.ownedPowerUps.Add(_powerUp.PowerUpID);
                    SaveSystem.Save(data);
                }

                Debug.Log($"Bought power-up: {_powerUp.PowerUpID}");
                UpdateButton(trophyManager.GetTrophies());
            }
            else
            {
                Debug.Log("Not enough trophies!");
            }
        }
    }
}
