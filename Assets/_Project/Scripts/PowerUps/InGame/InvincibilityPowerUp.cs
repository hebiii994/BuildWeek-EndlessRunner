using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New In-Game Power-Up", menuName = "Power-Ups/InGame/Invincibility Power-Up")]
public class InvincibilityPowerUp : AbstractPowerUp
{

    private float _powerUpDuration = 5f;
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
        // Implementa l'effetto specifico del power-up qui
        Debug.Log($"Power-up {PowerUpID} applied to {player.name} for {PowerUpDuration} seconds.");
        // Esempio: Aumenta la velocità del giocatore per la durata del power-up
        // PlayerController controller = player.GetComponent<PlayerController>();
        // if (controller != null)
        // {
        //     controller.StartCoroutine(ApplySpeedBoost(controller));
        // }
    }

}
