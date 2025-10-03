using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Rewards : MonoBehaviour
{
    [Header("Classification")]
    [SerializeField] string visualName;
    [SerializeField] string description;
    [SerializeField] int cost;
    [SerializeField] ItemType type;
    [SerializeField] BuyOrSell type2;

    [Header("UI Elements")]
    [SerializeField] Text nameText;
    [SerializeField] Text descriptionText;
    [SerializeField] TMP_Text costText;
    [SerializeField] Toggle toggle;

    private void Start()
    {
        nameText.text = visualName;
        descriptionText.text = description;
        costText.text = cost.ToString();
    }

    bool used = false;

    enum ItemType { Outfit, Buff }
    enum BuyOrSell { Buy, Sell}

    private bool BuyItem()
    {
        if (used) return true;

        if (Globals.Instance.score >= cost)
        {
            // Buy item
            Globals.Instance.score -= cost;
            if (type == ItemType.Outfit) { AddOutfitToList(); }

            // Disable Item from being bought again
            used = true; toggle.interactable = false;

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
