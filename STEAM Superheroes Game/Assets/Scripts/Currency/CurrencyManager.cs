using UnityEngine;

public class CurrencyManager : MonoBehaviour
{

    Rewards selectedReward;

    public void OnBuyItem()
    {
        bool transactionComplete = selectedReward.PerformAction();

        if (transactionComplete)
        {
            // display "bought" message
        }
        else
        {
            // display "not enough funds" message
        }
    }

    public void OnSelectItem(Rewards newReward)
    {
        selectedReward = newReward;
    }
}
