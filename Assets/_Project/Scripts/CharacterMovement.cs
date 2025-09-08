using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour
{
    [Header("Movimento base")]
    public float forwardSpeed = 5f;      // Velocità iniziale
    public float laneDistance = 3f;      // Distanza tra corsie
    private int currentLane = 1;         // 0 = sinistra, 1 = centro, 2 = destra

    [Header("Salto & Scivolata")]
    public float jumpForce = 7f;
    public float gravity = -20f;
    private float verticalVelocity;
    private bool isSliding = false;

    [Header("Riferimenti")]
    private CharacterController controller;
    private Transform modelTransform;

    [Header("Grandezze")]
    private float originalHeight;
    private Vector3 originalCenter;

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
        // Avanzamento costante
        Vector3 move = Vector3.forward * forwardSpeed;

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
        if (controller.isGrounded)
        {
            verticalVelocity = -1f;
            if (Input.GetKeyDown(KeyCode.Space))
                verticalVelocity = jumpForce;
            if (Input.GetKeyDown(KeyCode.LeftShift))
                StartCoroutine(Slide());
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        move.y = verticalVelocity;

        // Movimento finale
        controller.Move(move * Time.deltaTime);

        // Aumenta velocità nel tempo 
         forwardSpeed += Time.deltaTime * 0.1f;
    }

    IEnumerator Slide()
    {
        if (isSliding) yield break;
        isSliding = true;

        float targetHeight = originalHeight / 2f;
        Vector3 targetCenter = originalCenter / 2f;
        Vector3 originalPos = modelTransform.localPosition;
        Vector3 targetPos = originalPos + new Vector3(0, -originalHeight / 2f, 0);

        // Abbassamento fluido
        float elapsed = 0f;
        float duration = 0.2f;
        while (elapsed < duration)
        {
            controller.height = Mathf.Lerp(controller.height, targetHeight, elapsed / duration);
            controller.center = Vector3.Lerp(controller.center, targetCenter, elapsed / duration);
            modelTransform.localPosition = Vector3.Lerp(modelTransform.localPosition, targetPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        controller.height = targetHeight;
        controller.center = targetCenter;
        modelTransform.localPosition = targetPos;

        // Durata slide
        yield return new WaitForSeconds(0.6f);

        // Ripristino fluido
        elapsed = 0f;
        while (elapsed < duration)
        {
            controller.height = Mathf.Lerp(controller.height, originalHeight, elapsed / duration);
            controller.center = Vector3.Lerp(controller.center, originalCenter, elapsed / duration);
            modelTransform.localPosition = Vector3.Lerp(modelTransform.localPosition, originalPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        controller.height = originalHeight;
        controller.center = originalCenter;
        modelTransform.localPosition = originalPos;

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
