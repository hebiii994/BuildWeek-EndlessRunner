using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public string username;
    public List<float> highScores;
    public int value;

    //Nota bene: Ho messo GameObject per farmi capire che sono oggetti di gioco,
    //aggiornerò dopo che avremo definito una classe power-up
    public GameObject[] powerUps;




    public void UpdateHighScores(float newScore, int maxScores = 10)
    {
        if (highScores == null)
            highScores = new List<float>();

        highScores.Add(newScore);
        highScores.Sort((a, b) => b.CompareTo(a)); // Ordine decrescente

        // Mantieni solo i migliori punteggi
        if (highScores.Count > maxScores)
            highScores.RemoveRange(maxScores, highScores.Count - maxScores);
    }
}
