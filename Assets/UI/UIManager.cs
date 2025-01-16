using TMPro;
using Unity;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI treasuresText;
    public HealthbarScript healthBarScript;

    public void Start()
    {
        ShowGameUI(false);
        SetHealth(PlayerState.MaximumHealth);
    }

    public void SetTreasureCount(int val)
    {
        if (treasuresText != null)
        {
            treasuresText.SetText(string.Format("TREASURES LEFT: {0}", val));
        }
    }
    
    public void SetHealth(int val) 
    {
        healthBarScript.SetHearts(val);
    }

    public void ShowGameUI(bool show)
    {
        treasuresText.enabled = show;
        healthBarScript.Show(show);
    }
}
