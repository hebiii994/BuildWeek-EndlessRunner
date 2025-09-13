using UnityEngine;

public class Collectible : MonoBehaviour
{
<<<<<<< HEAD
    [Header("Effetti")]
    [SerializeField] private ParticleSystem _collectEffectPrefab;
    [SerializeField] private AudioClip _collectSound;
    [SerializeField] private int _scoreValue = 10;
=======
<<<<<<< Updated upstream
    [SerializeField] private int _scoreValue = 1; 

    // [SerializeField] private AudioClip _collectSound;
    // [SerializeField] private GameObject _collectEffect;
=======
    [Header("Effetti")]
    [SerializeField] private ParticleSystem _collectEffectPrefab;
    [SerializeField] private AudioClip _collectSound;
    [SerializeField] private int _amount;
>>>>>>> Stashed changes
>>>>>>> parent of 8f306f9 (Revert "aggiunte canvas menu e logica trofei/palloni")

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Collect();
        }
<<<<<<< Updated upstream
=======

        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateTrophyUI(_amount);
        }

        gameObject.SetActive(false);
>>>>>>> Stashed changes
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