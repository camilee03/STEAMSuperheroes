using UnityEngine;

public class CircuitsManager : MonoBehaviour
{
    //Manager for game state of circuits

    [SerializeField] int requiredSuccesses = 3;
    [SerializeField] WinLevel winLevel = null;
    [Header("DEBUG")]
    [SerializeField] int currentSuccesses = 0;

    //Add success toward level completion
    public void AddSuccess()
    {
        currentSuccesses++;
        if(currentSuccesses >= requiredSuccesses)
        {
            TriggerWinGame();
        }
    }
    //Remove a success
    public void RemoveSuccess()
    {
        currentSuccesses--;
    }
    //Trigger the level completion
    public void TriggerWinGame()
    {
        Debug.Log("Won the level");
        winLevel.ActivateCanvas();
    }
}
