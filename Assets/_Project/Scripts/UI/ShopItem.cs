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
    }

    private void OnDisable()
    {
        if (TrophyManager.Instance != null)
            TrophyManager.Instance.OnTrophyChanged -= UpdateButton;

        _buyButton.onClick.RemoveListener(Buy);
    }

    private void UpdateButton(int currentTrophies)
    {
        _buyButton.interactable = currentTrophies >= _price;
    }

    private void Buy()
    {
        if (TrophyManager.Instance.SpendTrophies(_price))
        {
            Debug.Log($"Acquistato {_itemName} per {_price} trofei!");
        }
    }
}
