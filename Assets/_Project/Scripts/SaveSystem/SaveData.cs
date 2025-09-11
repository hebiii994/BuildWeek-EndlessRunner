using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public string username;
    public List<float> highScores;
    public int value;
    //Aggiungo quest'altra variabile per tenere traccia del giocatore
    //Cos� da poter aggiornare i power-up applicati
    public GameObject _playerPreFab;
    //Nota bene: Ho messo GameObject per farmi capire che sono oggetti di gioco,
    //aggiorner� dopo che avremo definito una classe power-up
    public List<string> powerUpsID; // Lista degli ID dei power-up acquistati




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
