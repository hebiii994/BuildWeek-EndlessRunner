using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movimento base")]
    public float forwardSpeed = 20f;      // Velocità iniziale
    public float laneDistance = 3f;      // Distanza tra corsie
    private int currentLane = 1;         // 0 = sinistra, 1 = centro, 2 = destra
    public float speed_multiplier = 0.1f;

    [Header("Salto & Scivolata")]
    public float jumpForce = 7f;
    public float gravity = -20f;
    private float verticalVelocity;
    private bool isSliding = false;

    [Header("Ground Check Custom")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.2f;
    [SerializeField] private LayerMask groundMask;
    private bool isGrounded;

    [Header("Riferimenti")]
    private CharacterController controller;
    private Transform modelTransform;
    [SerializeField] private Animator animator;


    [Header("Grandezze")]
    private float originalHeight;
    private Vector3 originalCenter;

    [Header("Statistiche")]
    [SerializeField] private float metersTraveled = 0f;
    [SerializeField] private float timeElapsed = 0f;


    void Awake()
    {
        controller = GetComponent<CharacterController>();
        if (controller == null)
            controller = gameObject.AddComponent<CharacterController>();

        // Trova il child "Collider" ovunque nella gerarchia
        modelTransform = FindChildByName(transform, "Collider");
        if (modelTransform != null)
        {
            Collider modelCollider = modelTransform.GetComponent<Collider>();
            if (modelCollider != null)
            {
                // Prendo dimensioni locali rispetto al pivot
                originalHeight = modelCollider.bounds.size.y;
                originalCenter = modelCollider.transform.localPosition;
                controller.height = originalHeight;
                controller.center = originalCenter;
                controller.radius = Mathf.Max(modelCollider.bounds.size.x, modelCollider.bounds.size.z) / 2f;

            }
            else
            {
                originalHeight = controller.height;
                originalCenter = controller.center;
            }
        }
        else
        {
            originalHeight = controller.height;
            originalCenter = controller.center;
        }
    }

    void Update()
    {
        // nuovo ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Avanzamento costante
        Vector3 move = Vector3.forward * forwardSpeed;

        // Aggiorna statistiche
        metersTraveled = transform.position.z;
        timeElapsed += Time.deltaTime;

        // Gestione corsie
        if (Input.GetKeyDown(KeyCode.A) && currentLane > 0) currentLane--;
        if (Input.GetKeyDown(KeyCode.D) && currentLane < 2) currentLane++;

        // Posizione target della corsia
        Vector3 targetPosition = transform.position.z * Vector3.forward;
        if (currentLane == 0)
            targetPosition += Vector3.left * laneDistance;
        else if (currentLane == 2)
            targetPosition += Vector3.right * laneDistance;

        // Muovi lateralmente verso la corsia
        move.x = (targetPosition - transform.position).x * 10f;

        // Salto e scivolata
        if (isGrounded)
        {
            // Se siamo a terra e la velocità verticale è negativa, la resettiamo per "ancorarci"
            if (verticalVelocity < 0)
            {
                verticalVelocity = -2f;
            }

            // Gestione degli input quando si è a terra
            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity = jumpForce;
                if (animator != null) animator.SetTrigger("Jump");
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                StartCoroutine(Slide());
                if (animator != null) animator.SetTrigger("Slide");
            }
        }
        else
        {
            // Applica la gravità solo quando il personaggio è in aria
            verticalVelocity += gravity * Time.deltaTime;
        }

        move.y = verticalVelocity;

        // Movimento finale
        controller.Move(move * Time.deltaTime);

        // Aumenta velocità nel tempo 
         forwardSpeed += Time.deltaTime * speed_multiplier;
    }
    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
        }
    }
    IEnumerator Slide()
    {
        if (isSliding) yield break;
        isSliding = true;

        controller.height = originalHeight / 2f;
        controller.center = originalCenter / 2f;
        Debug.Log("Height: " + controller.height + " | Center: " + controller.center);
        Debug.Log("OHeight: " + originalHeight + " | OCenter: " + originalCenter);
        yield return new WaitForSeconds(0.6f);

        controller.height = originalHeight;
        controller.center = originalCenter;

        isSliding = false;
    }


    // Funzione ricorsiva per trovare un child ovunque nella gerarchia
    Transform FindChildByName(Transform parent, string name)
    {
        foreach (Transform t in parent)
        {
            if (t.name == name) return t;
            Transform found = FindChildByName(t, name);
            if (found != null) return found;
        }
        return null;
    }
}
