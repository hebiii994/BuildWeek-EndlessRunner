using UnityEngine;

public class BiomeManager : MonoBehaviour
{

    [SerializeField] private int _roadsSpawnedCounter = 0;

    [Header("Biome Tags")]
    [SerializeField] private string _stadiumTag;
    [SerializeField] private string _parkingTag;
    [SerializeField] private string _roadTag;

    public void IncrementRoadCounter()
    {
        _roadsSpawnedCounter++;
    }

    public void ResetCounter()
    {
        _roadsSpawnedCounter = 0;
    }

    public string GetCurrentBiomeTag()
    {
        // I pezzi da 0 a 10 sono Stadium
        if (_roadsSpawnedCounter <= 10)
        {
            return _stadiumTag;
        }
        // I pezzi da 11 a 20 sono Parking
        else if (_roadsSpawnedCounter <= 20)
        {
            return _parkingTag;
        }
        // Tutti i pezzi successivi sono Road
        else
        {
            return _roadTag;
        }
    }
}