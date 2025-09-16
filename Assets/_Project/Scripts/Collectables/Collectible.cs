using UnityEngine;

public class Collectible : MonoBehaviour

{ 
    [SerializeField] private ParticleSystem _collectEffectPrefab;
    [SerializeField] private AudioClip _collectSound;
    [SerializeField] private int _trophyAmount = 1;


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

        if (TrophyManager.Instance != null)
        {
            TrophyManager.Instance.AddTrophies(_trophyAmount);
        }

        gameObject.SetActive(false);
    }
}