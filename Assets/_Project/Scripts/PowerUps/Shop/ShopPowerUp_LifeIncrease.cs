using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(fileName = "PowerUp", menuName = "ScriptableObject/PowerUp/Shop/LifeIncrease")]
public class ShopPowerUp_LifeIncrease : ShopPowerUp
{
    [SerializeField] private int _lifeIncreaseAmount = 1; // Quantità di vita da aumentare
    public override void ApplyEffect(GameObject player)
    {
        if (player != null)
        {
            //Decommentare quanto sotto per far funzionare l'incremento della vita

            //LifeController lc = player.GetComponent<LifeController>();
            //if (lc != null)
            //{
            //    lc.IncreaseMaxLife(_lifeIncreaseAmount);
            //    Debug.Log($"Max life increased by {_lifeIncreaseAmount}. New max life: {lc.MaxLife}");
            //}
            //else
            //{
            //    Debug.LogWarning($"{this.name} could not find the LifeController component on player.");
            //}

            base.ApplyEffect(player);
        }
    }
}
