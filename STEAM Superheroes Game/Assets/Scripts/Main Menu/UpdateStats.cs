using TMPro;
using UnityEngine;

public class UpdateStats : MonoBehaviour
{
    [SerializeField] TMP_Text currencyText;

    private void Update()
    {
        currencyText.text = "Currency: " + Globals.Instance.score.ToString();
    }
}
