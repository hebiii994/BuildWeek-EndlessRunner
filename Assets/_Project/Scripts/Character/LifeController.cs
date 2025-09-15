using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class LifeController : MonoBehaviour
{
    [Header("Vita")]
    [SerializeField] private int maxLives = 3;
    [SerializeField] private int currentLives;

    [Header("Invincibilità")]
    [SerializeField] private float invincibleTime = 2f;
    private bool isInvincible = false;

    [Header("Danno e Rallentamento")]
    [Range(0f, 1f)]
    [SerializeField] private float speedPenaltyPercent = 0.1f; // 10% = 0.1

    private PlayerController playerController;

    void Awake()
    {
        playerController = GetComponent<PlayerController>();
        currentLives = maxLives;
    }

    

    public void TakeDamage()
    {
        if (isInvincible) return;

        currentLives--;

        // Se ancora vivo -> rallenta e diventa invincibile per un po'
        if (currentLives > 0)
        {
            ApplySpeedPenalty();
            StartCoroutine(Invincibility());
        }
        else
        {
            Die();
        }
    }
    public void GainMaxLife(int amount = 1)
    {
        maxLives += amount;
    }

    public void GainLife(int amount = 1)
    {
        currentLives = Mathf.Min(currentLives + amount, maxLives);
    }

    private void ApplySpeedPenalty()
    {
        float penalty = playerController.forwardSpeed * speedPenaltyPercent;
        playerController.forwardSpeed -= penalty;

        if (playerController.forwardSpeed < 5f) // non scendere sotto una minima velocità
            playerController.forwardSpeed = 5f;

        Debug.Log("Nuova velocità: " + playerController.forwardSpeed);
    }

    private IEnumerator Invincibility()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibleTime);
        isInvincible = false;
    }

    private void Die()
    {
        Debug.Log("Game Over!");
        GameManager.Instance.EndGame();
    }
}
