using UnityEngine;

[CreateAssetMenu(fileName = "Invincibility PowerUp", menuName = "ScriptableObjects/PowerUp/InGame/Invincibility Power-Up")]
public class InvincibilityPowerUp : AbstractPowerUp
{
    [SerializeField] private float _powerUpDuration = 5f;
    public float PowerUpDuration => _powerUpDuration;

    private Collider _collider;

    private void OnEnable()
    {
        // Assicuriamoci che il prefab abbia un collider trigger
        if (PowerUpPrefab != null)
        {
            _collider = PowerUpPrefab.GetComponent<Collider>();
            if (_collider == null)
            {
                Debug.LogWarning($"Collider not found on {PowerUpPrefab.name}. Adding a BoxCollider by default.");
                _collider = PowerUpPrefab.AddComponent<BoxCollider>();
            }

            _collider.isTrigger = true;
        }
    }

    public override void ApplyEffect(GameObject player)
    {
        // Usa il LifeController del player
        LifeController lc = player.GetComponentInParent<LifeController>();
        if (lc != null)
        {
            lc.StartCoroutine("Invincibility"); // mantiene la logica già esistente del tuo collega
            Debug.Log($"Power-up {PowerUpID} applied to {player.name} for {_powerUpDuration} seconds.");
        }
        else
        {
            Debug.LogWarning($"No LifeController found on {player.name}, cannot apply Invincibility power-up.");
        }

        // Salvataggio stato possesso power-up
        SaveData data = SaveSystem.Load();
        if (data != null && !data.ownedPowerUps.Contains(PowerUpID))
        {
            data.ownedPowerUps.Add(PowerUpID);
            SaveSystem.Save(data);
        }
    }
}
