using UnityEngine;

public class CircuitsManager : MonoBehaviour
{
    [SerializeField] int requiredSuccesses = 3;
    [SerializeField] WinLevel winLevel = null;
    [Header("DEBUG")]
    [SerializeField] int currentSuccesses = 0;

    public void AddSuccess()
    {
        currentSuccesses++;
        if(currentSuccesses >= requiredSuccesses)
        {
            TriggerWinGame();
        }
    }
    public void RemoveSuccess()
    {
        currentSuccesses--;
    }
    public void TriggerWinGame()
    {
        Debug.Log("Won the level");
        winLevel.ActivateCanvas();
    }
}
