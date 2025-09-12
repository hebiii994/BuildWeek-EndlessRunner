using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractPowerUp : ScriptableObject
{
    [SerializeField] private GameObject _powerUpPrefab;
    [SerializeField] private string _powerUpID; // Identificatore univoco

    public GameObject PowerUpPrefab => _powerUpPrefab;
    public string PowerUpID => _powerUpID;

    // Metodo astratto per applicare l'effetto specifico del power-up
    // Nel caso di acquisto sarà aggiunto al player
    //Nel caso di applicazione in-game sarà attivato e basta
    public abstract void ApplyEffect(GameObject player);

    //Questo sotto va aggiunto al metodo di acquisto del power-up
   
}
