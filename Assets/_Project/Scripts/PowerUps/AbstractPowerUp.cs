using UnityEngine;

public abstract class AbstractPowerUp : ScriptableObject
{
    [SerializeField] private GameObject _powerUpPrefab;
    [SerializeField] private string _powerUpID; // Identificatore unico
    [SerializeField] private int _cost; // Prezzo nello shop

    public GameObject PowerUpPrefab => _powerUpPrefab;
    public string PowerUpID => _powerUpID;
    public int Cost => _cost;

    // Metodo astratto per applicare l’effetto
    public abstract void ApplyEffect(GameObject player);
}
