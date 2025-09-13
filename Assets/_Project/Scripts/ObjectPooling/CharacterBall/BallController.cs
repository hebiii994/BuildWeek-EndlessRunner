using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody rb;
    private Transform startPoint;
    private bool returning = false;
    private PlayerBallManager owner;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Lancia il pallone verso la direzione cliccata, aggiungendo la velocità del player
    public void Launch(Vector3 direction, PlayerBallManager manager, float playerSpeed)
    {
        owner = manager;
        startPoint = manager.hitPoint;
        returning = false;

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        rb.AddForce(direction * 15f, ForceMode.Impulse);
        rb.AddForce(Vector3.forward * playerSpeed, ForceMode.VelocityChange);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Quando colpisce qualcosa, torna indietro
        returning = true;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        StartCoroutine(ReturnToPlayer());
    }

    private System.Collections.IEnumerator ReturnToPlayer()
    {
        while (Vector3.Distance(transform.position, startPoint.position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPoint.position, 30f * Time.deltaTime);
            yield return null;
        }

        owner.ReturnBall(this);
    }
}
