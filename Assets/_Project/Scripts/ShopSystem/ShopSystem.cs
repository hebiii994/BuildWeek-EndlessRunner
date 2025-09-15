using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSystem : MonoBehaviour
{    
    [SerializeField] private AbstractPowerUp[] _allPowerUpsArray; // Per l'inspector
    [SerializeField] private GameObject _playerPrefab;

    public Dictionary<string, AbstractPowerUp> _powerUpDictionary;
    private SaveData _saveData;


    private void Awake()
    {
        InitializePowerUpDictionary();
    }

    private void Start()
    {
        _saveData = SaveSystem.Load();
        if (_saveData == null)
        {
            Debug.LogError("Failed to load save data.");
        }
    }

    private void InitializePowerUpDictionary()
    {
        _powerUpDictionary = new Dictionary<string, AbstractPowerUp>();

        foreach (var powerUp in _allPowerUpsArray)
        {
            if (powerUp != null && !string.IsNullOrEmpty(powerUp.PowerUpID))
            {
                _powerUpDictionary[powerUp.PowerUpID] = powerUp;
            }
        }
    }

    public bool TryGetPowerUp(string powerUpID, out AbstractPowerUp powerUp)
    {
        return _powerUpDictionary.TryGetValue(powerUpID, out powerUp);
    }

    public void BuyPowerUp(ShopPowerUp powerUp, GameObject player)
    {
        if (_saveData == null)
        {
            Debug.LogError("SaveData is null. Cannot buy power-up.");
            return;
        }

        if (_saveData.playerCoins < powerUp.Cost)
        {
            Debug.Log("Non hai abbastanza valuta per acquistare questo power-up.");
            return;
        }
        else
        {
            // Deduce il costo dal valore del giocatore
            _saveData.playerCoins -= powerUp.Cost;
            _saveData.ownedPowerUp.Add(powerUp.PowerUpID);
            // Applica il power-up al giocatore
            // La logica di salvataggio è in applyEffect
            powerUp.ApplyEffect(player);
        }

    }
}
