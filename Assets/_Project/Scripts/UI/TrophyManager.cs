using UnityEngine;
using System;

public class TrophyManager : MonoBehaviour
{
    public static TrophyManager Instance;

    public event Action<int> OnTrophyChanged;

    private int _totalTrophies = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            _totalTrophies = PlayerPrefs.GetInt("TotalTrophies", 0);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int GetTrophies() => _totalTrophies;

    public void AddTrophies(int amount)
    {
        _totalTrophies += amount;
        Save();
        OnTrophyChanged?.Invoke(_totalTrophies);
    }

    public bool SpendTrophies(int amount)
    {
        if (_totalTrophies >= amount)
        {
            _totalTrophies -= amount;
            Save();
            OnTrophyChanged?.Invoke(_totalTrophies);
            return true;
        }
        return false;
    }

    private void Save()
    {
        PlayerPrefs.SetInt("TotalTrophies", _totalTrophies);
        PlayerPrefs.Save();
    }
}
