using UnityEngine;

public class PowerUpCollector : MonoBehaviour
{
    [SerializeField] private AbstractPowerUp _powerUp;

    private SaveData data;

    private void Start()
    {
        data = SaveSystem.Load();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Collect(other.gameObject);
        }
    }

    private void Collect(GameObject player)
    {
        if (_powerUp != null)
        {
            _powerUp.ApplyEffect(player);

            if (!data.ownedPowerUps.Contains(_powerUp.PowerUpID))
            {
                data.ownedPowerUps.Add(_powerUp.PowerUpID);
                SaveSystem.Save(data);
            }

            Debug.Log($"Collected power-up: {_powerUp.PowerUpID}");
            Destroy(gameObject);
        }
    }
}
