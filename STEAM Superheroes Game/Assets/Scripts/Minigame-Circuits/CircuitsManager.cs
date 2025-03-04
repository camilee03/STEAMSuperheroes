using UnityEngine;

public class CircuitsManager : MonoBehaviour
{
    [SerializeField] int requiredSuccesses = 3;
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
        //TODO - win the game
        Debug.Log("Win the Game");
    }
}
