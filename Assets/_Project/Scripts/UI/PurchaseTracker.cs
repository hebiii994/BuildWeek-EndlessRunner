using UnityEngine;
using System.Collections.Generic;

public class PurchaseTracker : MonoBehaviour
{
    public static PurchaseTracker Instance;

    private List<string> purchasedItems = new List<string>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Load();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool IsPurchased(string itemName)
    {
        return purchasedItems.Contains(itemName);
    }

    public void Purchase(string itemName)
    {
        if (!purchasedItems.Contains(itemName))
        {
            purchasedItems.Add(itemName);
            PlayerPrefs.SetInt("ShopItem_" + itemName, 1);
            PlayerPrefs.Save();
        }
    }

    private void Load()
    {
        string[] allItems = { "PowerUp1", "PowerUp2", "PowerUp3" };
        purchasedItems.Clear();

        foreach (string item in allItems)
        {
            if (PlayerPrefs.GetInt("ShopItem_" + item, 0) == 1)
            {
                purchasedItems.Add(item);
            }
        }
    }
}
