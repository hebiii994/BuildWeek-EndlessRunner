using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeManager : MonoBehaviour
{
    public float _roadSoFar = 0;
    [SerializeField] private enum RoadBiomes { STADIUM, PARKING, ROAD};
    [SerializeField] private string _stadiumString;
    [SerializeField] private string _parkingString;
    [SerializeField] private string _roadString;



    public string UpdateBiome()
    {
        if (_roadSoFar <= 10)
        {
            return _stadiumString;
        }
        // I pezzi da 11 a 20 sono Parking
        else if (_roadSoFar <= 20)
        {
            return _parkingString;
        }
        // Tutti i pezzi successivi sono Road
        else
        {
            return _roadString;
        }
    }

}









