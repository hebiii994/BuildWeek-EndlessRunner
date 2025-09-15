using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpCollector : MonoBehaviour, iCollectable
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Collect();
        }
    }

    public void Collect()
    {
        // Qui puoi aggiungere la logica per applicare l'effetto del power-up al giocatore
        Debug.Log("Power-up collected!");
        // Distruggi il power-up dopo la raccolta
        Destroy(gameObject);
    }
}
