using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private int _scoreValue = 1; 

    // [SerializeField] private AudioClip _collectSound;
    // [SerializeField] private GameObject _collectEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Comunica all'UIManager di aggiungere punti
            if (UIManager.Instance != null)
            {
                UIManager.Instance.AddScore(_scoreValue);
            }

            // effetti sonori/visivi per il futuro
            // if (_collectSound) AudioSource.PlayClipAtPoint(_collectSound, transform.position);
            // if (_collectEffect) Instantiate(_collectEffect, transform.position, Quaternion.identity);

            gameObject.SetActive(false);
        }
    }
}