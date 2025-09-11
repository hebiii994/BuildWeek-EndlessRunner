using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[CreateAssetMenu(fileName = "PowerUp", menuName = "ScriptableObject/ShopPowerUp")]
public class ShopPowerUp : AbstractPowerUp
{
    [SerializeField] private int _cost; // Costo del power-up
    public int Cost => _cost;
    protected override void ApplyEffect(GameObject player)
    {        
        SavePowerUpState();
    }

    private void SavePowerUpState()
    {
        SaveData saveData = SaveSystem.Load() ?? new SaveData();

        if (saveData.powerUpsID == null)
            saveData.powerUpsID = new List<string>();

        if (!saveData.powerUpsID.Contains(PowerUpID))
        {
            saveData.powerUpsID.Add(PowerUpID);
            SaveSystem.Save(saveData);
            Debug.Log($"Power-up {PowerUpID} applied!");
        }
    }

}
