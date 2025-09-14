using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeManager : MonoBehaviour
{
    public float _roadSoFar;
    [SerializeField] private enum RoadBiomes { STADIUM, PARKING, ROAD};
    [SerializeField] private string _roadString;
    [SerializeField] private string _parkingString;
    [SerializeField] private string _streetString;



    public string UpdateBiome()
    {
        _roadSoFar++;
        switch (_roadSoFar)
        {
            case >= 0 and <= 2:
                return _roadString;
               

            case > 3 and <= 5:
                return _parkingString;
                

            case > 5:
                return _streetString;


            default:
                Debug.Log("Nessun Bioma");
                return _roadString;
        }
    }

}









