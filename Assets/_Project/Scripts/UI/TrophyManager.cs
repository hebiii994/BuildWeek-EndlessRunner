using UnityEngine;
using System;

public class TrophyManager : MonoBehaviour
{
    public static TrophyManager Instance;

    public event Action<int> OnTrophyChanged;

    private int totalTrophies = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            totalTrophies = PlayerPrefs.GetInt("TotalTrophies", 0);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int GetTrophies()
    {
        return totalTrophies;
    }

    public void AddTrophies(int amount)
    {
        totalTrophies += amount;
        Save();
        OnTrophyChanged?.Invoke(totalTrophies);
    }

    public bool SpendTrophies(int amount)
    {
        if (totalTrophies >= amount)
        {
            totalTrophies -= amount;
            Save();
            OnTrophyChanged?.Invoke(totalTrophies);
            return true;
        }
        return false;
    }

    private void Save()
    {
        PlayerPrefs.SetInt("TotalTrophies", totalTrophies);
        PlayerPrefs.Save();
    }
}
