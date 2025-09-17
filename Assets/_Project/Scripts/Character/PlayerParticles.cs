using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerParticles : MonoBehaviour
{
    [Header("Riferimenti")]
    [SerializeField] private ParticleSystem speedLinesEffect;

    private PlayerController playerController;
    private ParticleSystem.EmissionModule emissionModule;

    void Start()
    {
        playerController = GetComponent<PlayerController>();

        if (speedLinesEffect != null)
        {
            emissionModule = speedLinesEffect.emission;
            emissionModule.enabled = false;

        }
    }

    void Update()
    {
        if (speedLinesEffect == null) return;

        float speedThreshold = 21.5f;

        if (playerController.forwardSpeed > speedThreshold)
        {
            emissionModule.enabled = true;
            emissionModule.rateOverTime = playerController.forwardSpeed * 3;
        }
        else
        {
            emissionModule.enabled = false;
        }
    }
}