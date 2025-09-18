using System.Runtime.CompilerServices;
using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    [SerializeField] private int score;
    private void OnTriggerEnter(Collider other)
    {
        BallController ball = other.GetComponent<BallController>();
        
        if (ball != null)
        {
            // Avvisa il PlayerBallManager che hai fatto goal
            ball.owner.AddScore(score);

            // Rimuovi la palla dalla pool (non ritorna)
            ball.gameObject.SetActive(false);

            Debug.Log("GOAL!");
        }
    }
}
