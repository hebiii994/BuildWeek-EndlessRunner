using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopoController : MonoBehaviour
{
    private SaveData _saveData;
    // Array di tutti i power-up disponibili nel gioco
    //Ovviamente, va modificato il parametro in base alla classe che useremo per i power-up
    [SerializeField] private GameObject[] _allPowerUps;

    [SerializeField] private GameObject _playerPreFab;

    //public void BuyPowerUp(ScriptableObject powerUp)
    //{
    //    if (_saveData == null)
    //    {
    //        Debug.LogError("SaveData is null. Cannot buy power-up.");
    //        return;
    //    }
    //    int playerCurrency = _saveData.value;
    //    if (playerCurrency < powerUp.Cost)
    //    {
    //        Debug.Log("Non hai abbastanza valuta per acquistare questo power-up.");
    //        return;
    //    }

    //    // Deduce il costo dal valore del giocatore
    //    _saveData.value -= powerUp.Cost;
    //    // Aggiunge il power-up all'array dei power-up posseduti
    //    List<GameObject> updatedPowerUps = new List<GameObject>(_saveData.powerUps);
    //    updatedPowerUps.Add(powerUp.PowerUpPrefab);
    //    _saveData.powerUps = updatedPowerUps.ToArray();
    //    // Salva i dati aggiornati
    //    SaveSystem.Save(_saveData);
    //    Debug.Log($"Acquistato {powerUp.name}. Valore rimanente: {_saveData.value}");
    //}

}
