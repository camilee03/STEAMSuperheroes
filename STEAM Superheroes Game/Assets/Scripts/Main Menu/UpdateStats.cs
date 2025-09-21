using TMPro;
using UnityEngine;

public class UpdateStats : MonoBehaviour
{
    [SerializeField] TMP_Text currencyText;

    private void Update()
    {
        currencyText.text = $"Spend Coins ({Globals.Instance.score.ToString()})";
    }
}
