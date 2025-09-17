using UnityEngine;

public class PlayerAudioEvents : MonoBehaviour
{
    [Header("Suoni dei Passi")]
    [Tooltip("Lista di suoni per i passi. Ne verrà scelto uno a caso ad ogni passo.")]
    [SerializeField] private AudioClip[] footstepSounds;

    public void PlayFootstepSound()
    {
        Debug.Log("Evento Passo attivato al frame: " + Time.frameCount);
        if (footstepSounds == null || footstepSounds.Length == 0) return;

        int index = Random.Range(0, footstepSounds.Length);
        AudioClip clip = footstepSounds[index];

        if (AudioManager.Instance != null && clip != null)
        {
            AudioManager.Instance.PlaySound(clip);
        }
    }
}