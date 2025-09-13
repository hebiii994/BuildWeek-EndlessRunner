using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[CreateAssetMenu(fileName = "PowerUp", menuName = "ScriptableObject/PowerUp")]

public abstract class AbstractPowerUp : ScriptableObject
{
    [SerializeField] private GameObject _powerUpPrefab;
    [SerializeField] private string _powerUpID; // Identificatore univoco

    public GameObject PowerUpPrefab => _powerUpPrefab;
    public string PowerUpID => _powerUpID;

    public virtual void ApplyPowerUp(GameObject player)
    {
        // Applica l'effetto del power-up
        ApplyEffect(player);

        // Salva che questo power-up è attivo
        
    }

    // Metodo astratto per applicare l'effetto specifico del power-up
    // Nel caso di acquisto sarà aggiunto al player
    //Nel caso di applicazione in-game sarà attivato e basta
    protected abstract void ApplyEffect(GameObject player);

    //Questo sotto va aggiunto al metodo di acquisto del power-up
   
}
