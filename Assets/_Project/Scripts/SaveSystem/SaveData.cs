using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public string username;
    public List<float> highScores;
    public int playerCoins;

    public GameObject _playerPreFab;
    public List<string> ownedPowerUp;

    [System.Serializable]
    public class GameRecord
    {
        public float Meters;
        public float Time;

        public GameRecord(float meters, float time)
        {
            Meters = meters;
            Time = time;
        }
    }

    public List<GameRecord> lastGames = new List<GameRecord>();

    public void AddGameRecord(float meters, float time, int maxRecords = 5)
    {
        lastGames.Add(new GameRecord(meters, time));
        lastGames.Sort((a, b) => b.Meters.CompareTo(a.Meters));

        if (lastGames.Count > maxRecords)
            lastGames.RemoveRange(maxRecords, lastGames.Count - maxRecords);
    }

    //public void UpdateHighScores(float newScore, int maxScores = 5)
    //{
    //    if (highScores == null)
    //        highScores = new List<float>();

    //    highScores.Add(newScore);
    //    highScores.Sort((a, b) => b.CompareTo(a)); // Ordine decrescente
    //
    //    if (highScores.Count > maxScores)
    //        highScores.RemoveRange(maxScores, highScores.Count - maxScores);
    //}
}
