using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Invincibility PowerUp", menuName = "ScriptableObjects/PowerUp/InGame/Invincibility Power-Up")]
public class InvincibilityPowerUp : AbstractPowerUp
{

    private float _powerUpDuration = 5f; //Nota bene: Ho visto che la durata di invincibility è stata impostata nel Life Controller. 
                                         //Secondo me avrebbe più senso metterla qui, così da impostare un moltiplicatore
                                         //Lascio tutto così per evitare conflitti inutili su Git.
    public float PowerUpDuration => _powerUpDuration;

    Collider _collider;

    private void OnEnable()
    {
        _collider = PowerUpPrefab.gameObject.GetComponent<Collider>();
        if (_collider == null)
        {
            Debug.LogWarning($"Collider not found on {PowerUpPrefab.name}. Adding a BoxCollider by default.");
            _collider = PowerUpPrefab.gameObject.AddComponent<BoxCollider>();
            ((BoxCollider)_collider).isTrigger = true; // Imposta come trigger
        }
        else
        {
            _collider.isTrigger = true; // Assicurati che sia un trigger
        }
    }

    public override void ApplyEffect(GameObject player)
    {
        LifeController lc = player.GetComponent<LifeController>();
        if (lc != null)
        {
            lc.StartCoroutine("Invincibility");
            Debug.Log($"Power-up {PowerUpID} applied to {player.name}");
        }
        
        // Esempio: Aumenta la velocità del giocatore per la durata del power-up

    }
}
