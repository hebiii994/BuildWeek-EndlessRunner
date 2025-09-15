using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpCollector : MonoBehaviour, iCollectable
{
    [SerializeField] private AbstractPowerUp powerUp;

    private void Start()
    {
        // Verifica che il power-up sia assegnato
        if (powerUp == null)
        {
            Debug.LogWarning($"PowerUp ScriptableObject not assigned on {gameObject.name}");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Collect(other.gameObject); // Passa il player come parametro
        }
    }

    public void Collect()
    {
        // Implementazione base per compatibilità con iCollectable
        Collect(GameObject.FindGameObjectWithTag("Player"));
    }

    public void Collect(GameObject player)
    {
        if (powerUp == null)
        {
            Debug.LogError("PowerUp ScriptableObject is null!");
            return;
        }

        if (player == null || !player.CompareTag("Player"))
        {
            Debug.LogError("Player is null!");
            return;
        }

        // Applica l'effetto del power-up al giocatore
        powerUp.ApplyEffect(player);
        
        // Invoca l'evento se qualcuno è in ascolto
        powerUp.OnTakingPowerUp?.Invoke();

        Debug.Log($"Power-up {powerUp.PowerUpID} collected and applied to {player.name}!");
        
        // Distruggi il power-up dopo la raccolta
        Destroy(gameObject);
    }
}
