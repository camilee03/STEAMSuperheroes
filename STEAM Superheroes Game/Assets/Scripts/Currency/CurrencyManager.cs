using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyManager : MonoBehaviour
{
    [SerializeField] ToggleGroup storeToggleGroup;
    [SerializeField] ToggleGroup rewardToggleGroup;

    private void Start()
    {
        // change not achieved toggles to disabled
    }

    public void OnBuyItem()
    {
        if (!storeToggleGroup.AnyTogglesOn()) return; // display "no item selected" message

        IEnumerable<Toggle> toggles = storeToggleGroup.ActiveToggles();

        bool transactionComplete = true;


        // Find a way to "undo" buying
        foreach (Toggle toggle in toggles)
        {
            if (!toggle.gameObject.GetComponent<Rewards>().PerformAction()) { transactionComplete = false; break; }
        }

        if (transactionComplete)
        {
            // display "bought" message
            Debug.Log($"Bought items.");
        }
        else
        {
            // display "not enough funds" message
            Debug.Log($"Not enough funds.");
        }
    }
}
