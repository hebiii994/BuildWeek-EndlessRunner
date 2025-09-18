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
            transform.parent = null;
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
            Save();
        }
    }

    private void Save()
    {
        SaveData data = SaveSystem.Load() ?? new SaveData();
        data.ownedPowerUps = new List<string>(purchasedItems);
        SaveSystem.Save(data);
    }

    private void Load()
    {
        SaveData data = SaveSystem.Load() ?? new SaveData();
        purchasedItems = new List<string>(data.ownedPowerUps);
    }
}
