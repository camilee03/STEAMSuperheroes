using UnityEngine;

public class WinLevel : MonoBehaviour
{
    [SerializeField] int currencyAmountToAdd = 0;

    winscree
    public void AddCurrency()
    {
        Globals.Instance.score += currencyAmountToAdd;
    }

    public void ActivateCanvas()
    {

    }
}
