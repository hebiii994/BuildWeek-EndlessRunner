using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public string itemName;
    public int price;
    public Button buyButton;

    private void Start()
    {
        TrophyManager.Instance.OnTrophyChanged += UpdateButton;
        UpdateButton(TrophyManager.Instance.GetTrophies());

        buyButton.onClick.AddListener(Buy);
    }

    private void OnDestroy()
    {
        if (TrophyManager.Instance != null)
            TrophyManager.Instance.OnTrophyChanged -= UpdateButton;
    }

    private void UpdateButton(int currentTrophies)
    {
        buyButton.interactable = currentTrophies >= price;
    }

    private void Buy()
    {
        if (TrophyManager.Instance.SpendTrophies(price))
        {
            Debug.Log($"Acquistato {itemName} per {price} trofei!");

        }
    }
}
