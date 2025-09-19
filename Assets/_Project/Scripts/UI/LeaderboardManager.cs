using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaderboardManager : MonoBehaviour
{
    public static LeaderboardManager Instance;

    [System.Serializable]
    public class LeaderboardEntry
    {
        public float Meters;
        public float Time;

        public LeaderboardEntry(float meters, float time)
        {
            Meters = meters;
            Time = time;
        }
    }

    public int maxEntries = 5;

    public TMP_Text[] metersTexts;
    public TMP_Text[] timeTexts;

    private List<LeaderboardEntry> entries = new List<LeaderboardEntry>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
            UpdateLeaderboardFromSave();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateLeaderboardFromSave()
    {
        SaveData data = SaveSystem.Load();
        entries.Clear();

        if (data != null && data.lastGames != null)  
        {
            foreach (var record in data.lastGames)
            {
                entries.Add(new LeaderboardEntry(record.Meters, record.Time));
            }

            entries.Sort((a, b) =>
            {
                int cmp = b.Meters.CompareTo(a.Meters);
                return cmp != 0 ? cmp : a.Time.CompareTo(b.Time);
            });

            if (entries.Count > maxEntries)
                entries.RemoveRange(maxEntries, entries.Count - maxEntries);
        }

        UpdateUI();
    }

    public void AddEntryFromGame(float meters, float time)
    {
        SaveSystem.AddNewGame(meters, time);
        UpdateLeaderboardFromSave();
    }

    private void UpdateUI()
    {
        for (int i = 0; i < maxEntries; i++)
        {
            if (i < entries.Count)
            {
                metersTexts[i].text = entries[i].Meters.ToString("F1") + " m";
                timeTexts[i].text = entries[i].Time.ToString("F1") + " s";
            }
            else
            {
                metersTexts[i].text = "-";
                timeTexts[i].text = "-";
            }
        }
    }

    public List<LeaderboardEntry> GetEntries()
    {
        return entries;
    }
}
