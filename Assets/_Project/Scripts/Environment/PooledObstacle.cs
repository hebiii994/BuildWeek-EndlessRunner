using UnityEngine;
using UnityEngine.Pool;

public class PooledObstacle : MonoBehaviour
{

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collisione con ostacolo! GAME OVER");
            // GameManager.Instance.EndGame();
            Time.timeScale = 0;
        }
    }
}

