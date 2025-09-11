using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    // Questo metodo viene chiamato quando c'� una collisione
    public abstract void OnCollisionHappened(GameObject player);
}
