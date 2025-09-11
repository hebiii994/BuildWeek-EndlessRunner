using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private TMP_Text trophyCounterText;

    private void OnEnable()
    {
        UpdateTrophyUI(TrophyManager.Instance.GetTrophies());

        TrophyManager.Instance.OnTrophyChanged += UpdateTrophyUI;
    }

    private void OnDisable()
    {
        if (TrophyManager.Instance != null)
            TrophyManager.Instance.OnTrophyChanged -= UpdateTrophyUI;
    }

    private void UpdateTrophyUI(int total)
    {
        trophyCounterText.text = total.ToString();
    }
}
