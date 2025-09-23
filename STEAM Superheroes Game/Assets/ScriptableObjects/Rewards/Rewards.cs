using UnityEngine;

[CreateAssetMenu(fileName = "Rewards", menuName = "Scriptable Objects/Rewards")]
public class Rewards : ScriptableObject
{
    [SerializeField] string visualName;
    [SerializeField] string description;
    [SerializeField] int cost;
    [SerializeField] ItemType type;
    [SerializeField] BuyOrSell type2;
    bool used = false;

    enum ItemType { Outfit, Buff }
    enum BuyOrSell { Buy, Sell}

    private bool BuyItem()
    {
        if (used) return true;

        if (Globals.Instance.score >= cost)
        {
            Globals.Instance.score -= cost;
            used = true;
            if (type == ItemType.Outfit) { AddOutfitToList(); }
            return true;
        }
        else return false;
    }

    private bool GetReward()
    {
        if (used) return true;
        // -- TBI: if condition is met
        else return false;
    }

    public bool PerformAction()
    {
        if (type2 == BuyOrSell.Buy) { return BuyItem(); }
        else { return GetReward(); }
    }

    void AddOutfitToList()
    {
        // use globals for this
    }
}
