using UnityEngine;

public class CurvedWorldController : MonoBehaviour
{
    [SerializeField]
    private Transform playerTransform; 


    private static readonly int PlayerPositionID = Shader.PropertyToID("_PlayerPosition");

    void Update()
    {
        if (playerTransform == null) return;

        Shader.SetGlobalVector(PlayerPositionID, playerTransform.position);
    }
}