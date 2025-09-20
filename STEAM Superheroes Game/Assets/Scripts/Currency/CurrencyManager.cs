using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    struct Items
    {
        string name;
        int cost;
        ItemType type;
    }

    enum ItemType { Outfit, Buff }

    private void SpendCurrency(int amount)
    {
        Globals.Instance.score -= amount;
    }

    private void ClaimCurrency(int amount)
    {
        Globals.Instance.score += amount;
    }


}
