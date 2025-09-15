using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ShopPowerUp : AbstractPowerUp
{
    [SerializeField] private int _cost; // Costo del power-up
    public int Cost => _cost;
    public override void ApplyEffect(GameObject player)
    {
        SaveData data = SaveSystem.Load();
        SavePowerUpState(data, this);        
    }

    private void SavePowerUpState(SaveData data, AbstractPowerUp powerUp)
    {
        if (data == null || powerUp == null)
        {
            Debug.LogError("SaveData or PowerUp is null. Cannot save power-up state.");
            return;
        }
        if (!data.ownedPowerUp.Contains(powerUp.PowerUpID))
        {
            data.ownedPowerUp.Add(powerUp.PowerUpID);
            Debug.Log($"Power-up {powerUp.PowerUpID} added to owned power-ups.");
        }
        else
        {
            Debug.LogWarning($"Power-up {powerUp.PowerUpID} is already owned.");
        }

        SaveSystem.Save(data);
    }

}
