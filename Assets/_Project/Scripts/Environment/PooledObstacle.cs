using UnityEngine;
using UnityEngine.Pool;

public class PooledObstacle : Interactable
{

    public override void OnCollisionHappened(GameObject player)
    {
        // Recupera il LifeController del player
        LifeController life = player.GetComponent<LifeController>();
        if (life != null)
        {
            life.TakeDamage();
            Debug.Log("Player ha colpito un ostacolo!");
        }

        // Qui puoi aggiungere effetti specifici dell'ostacolo
        // ad esempio suoni, particelle, ecc.
    }

}

