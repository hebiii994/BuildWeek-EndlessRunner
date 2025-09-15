using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private string _itemName;
    [SerializeField] private int _price;
    [SerializeField] private Button _buyButton;

    private void OnEnable()
    {
        TrophyManager.Instance.OnTrophyChanged += UpdateButton;
        UpdateButton(TrophyManager.Instance.GetTrophies());

        _buyButton.onClick.AddListener(Buy);

        if (PurchaseTracker.Instance.IsPurchased(_itemName))
            _buyButton.interactable = false;
    }

    private void OnDisable()
    {
        if (TrophyManager.Instance != null)
            TrophyManager.Instance.OnTrophyChanged -= UpdateButton;

        _buyButton.onClick.RemoveListener(Buy);
    }

    private void UpdateButton(int currentTrophies)
    {
        if (PurchaseTracker.Instance.IsPurchased(_itemName))
        {
            _buyButton.interactable = false;
        }
        else
        {
            _buyButton.interactable = currentTrophies >= _price;
        }
    }

    private void Buy()
    {
        if (TrophyManager.Instance.SpendTrophies(_price))
        {
            Debug.Log($"Bought {_itemName} for {_price} Trophies!");
            PurchaseTracker.Instance.Purchase(_itemName);
            _buyButton.interactable = false;
        }
        else
        {
            Debug.Log("Not enough trophies to buy this power-up!");
        }
    }

}
