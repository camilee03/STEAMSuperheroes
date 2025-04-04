using UnityEngine;

public class WinLevel : MonoBehaviour
{
    [SerializeField] float levelCode = 0; //change this to whichever level you are on

    [SerializeField] int currencyAmountToAdd = 0;

    [SerializeField] GameObject winScreenCanvas = null;

    //This opens win screen and declares level won
    public void ActivateCanvas()
    {
        winScreenCanvas.SetActive(true);
        CompleteLevel();
    }
    public void DeactivateCanvas() { //Most Likely not going to be used
        winScreenCanvas.SetActive(false);
    }
    public void CompleteLevel() {
        //Check if the level is not already completed in Globals.Instance
        if (!Globals.Instance.levelsCompleted.Contains(levelCode)) {

            //Add to score
            Globals.Instance.score += currencyAmountToAdd;
            Debug.Log("Increased score by: " + currencyAmountToAdd);

            //Add completed level
            Globals.Instance.levelsCompleted.Add(levelCode);
            Debug.Log("Added " + levelCode + " to completed levels");
        }
    }
    public void ToMainMenu() {
        FindFirstObjectByType<SceneLoader>().LoadMainMenu();
    }
}
