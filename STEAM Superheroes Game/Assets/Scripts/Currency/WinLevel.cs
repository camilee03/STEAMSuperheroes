using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class WinLevel : MonoBehaviour
{
    //Handles level completion, and currency addition

    [Header("Level Specific - Be Sure to Change")]
    [SerializeField] float levelCode = 0; //change this to whichever level you are on in inspector
    public int currencyAmountToAdd = 0;

    [Header("References - Do Not Change")]
    [SerializeField] GameObject winScreenCanvas = null;
    [SerializeField] TextMeshProUGUI currencyText = null;

    //This opens win screen and declares level won. Called by other scripts
    public void ActivateCanvas()
    {
        winScreenCanvas.SetActive(true);
        currencyText.text = "";
        CompleteLevel();
    }
    //Closes win screen canvas
    public void DeactivateCanvas() { 
        winScreenCanvas.SetActive(false);
    }
    //Add to level completion in Glboals
    public void CompleteLevel() {
        //Check if the level is not already completed in Globals.Instance
        if (!Globals.Instance.levelsCompleted.Contains(levelCode)) {
            // Check if the level is a final level
            if (levelCode.ToString().StartsWith('6'))
            {
                Debug.Log("One of final level completed");
                winScreenCanvas.SetActive(false);

                currencyText.text = "Completed!";
                Globals.Instance.levelsCompleted.Add(levelCode);

                // Load next level automatically
                if (SceneManager.sceneCount < SceneManager.GetActiveScene().buildIndex + 1) 
                { 
                    SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1); 
                }
                else { SceneManager.LoadSceneAsync(2); }
                
            }
            else
            {
                //Show Text - TODO - update to visual intstead of text
                currencyText.text = "Gained " + currencyAmountToAdd + " Currency!";

                //Add to score
                Globals.Instance.score += currencyAmountToAdd;
                Debug.Log("Increased score by: " + currencyAmountToAdd);

                //Add completed level
                Globals.Instance.levelsCompleted.Add(levelCode);
                Debug.Log("Added " + levelCode + " to completed levels");
            }
        }
    }
    //Load Main Menu
    public void ToMainMenu() {
        DeactivateCanvas();
        FindFirstObjectByType<SceneLoader>().LoadMainMenu();
    }
}
