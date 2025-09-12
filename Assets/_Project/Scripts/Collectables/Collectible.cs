using UnityEngine;

public class Collectible : MonoBehaviour
{
    [Header("Effetti")]
    [SerializeField] private ParticleSystem _collectEffectPrefab;
    [SerializeField] private AudioClip _collectSound;
    [SerializeField] private int _scoreValue = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Collect();
        }
    }

    private void Collect()
    {
        if (_collectSound != null && AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySound(_collectSound);
        }

        if (_collectEffectPrefab != null)
        {
            Instantiate(_collectEffectPrefab, transform.position, Quaternion.identity);
        }

        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateScore(_scoreValue);
        }

        gameObject.SetActive(false);
    }
}