using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour
{
    private Rigidbody rb;
    private Transform startPoint;
    private bool returning = false;
    private PlayerBallManager owner;

    private float maxX; // limite laterale del campo
    private float minY = -10f; // altezza minima per considerare persa la palla

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Lancia il pallone verso la direzione cliccata, velocità uguale al player
    public void Launch(Vector3 direction, PlayerBallManager manager, float playerSpeed)
    {
        owner = manager;
        startPoint = manager.hitPoint;
        returning = false;

        // Imposto il limite laterale corretto
        maxX = manager.playerController.laneDistance;

        rb.velocity = direction.normalized * playerSpeed;
        rb.angularVelocity = Vector3.zero;

        // Avvia il controllo per la palla persa
        StartCoroutine(CheckOutOfBounds());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!returning)
        {
            returning = true;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            StartCoroutine(ReturnToPlayer());
        }
    }

    private IEnumerator ReturnToPlayer()
    {
        while (Vector3.Distance(transform.position, startPoint.position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPoint.position, 30f * Time.deltaTime);
            yield return null;
        }

        owner.ReturnBall(this);
    }

    private IEnumerator CheckOutOfBounds()
    {
        while (!returning)
        {
            // Controlla se la palla esce dai limiti laterali o scende sotto l'altezza minima
            if (Mathf.Abs(transform.position.x) > maxX || transform.position.y < minY)
            {
                Destroy(gameObject); // palla persa
                yield break;
            }

            yield return null;
        }
    }
}
