using UnityEngine;
using TMPro;

public class ShopController : MonoBehaviour
{
    [SerializeField] private TMP_Text _trophyCounterText;

    private void Start()
    {
   
        UpdateTrophyUI(TrophyManager.Instance.GetTrophies());
        TrophyManager.Instance.OnTrophyChanged += UpdateTrophyUI;
        
    }

    private void OnDestroy()
    {
        if (TrophyManager.Instance != null)
            TrophyManager.Instance.OnTrophyChanged -= UpdateTrophyUI;
    }

    private void UpdateTrophyUI(int total)
    {
        _trophyCounterText.text = total.ToString();
    }
}
