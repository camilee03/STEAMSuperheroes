using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    //This class handles all functions and methods for the pause menu

    [SerializeField] GameObject pauseMenuCanvas = null;
    [SerializeField] int[] doNotOpenSceneIdx = null;
    bool pauseMenuOpen = false;
    public static PauseMenu PauseMenuInstance { get; private set; }

    //Create the singleton
    private void Awake()
    {
        //Singleton
        if (PauseMenuInstance != null && PauseMenuInstance != this)
        {
            Destroy(this);
        } else
        {
            PauseMenuInstance = this;
        }
        DontDestroyOnLoad(gameObject);

        pauseMenuCanvas.SetActive(false);
    }

    //Detect Key Input to Open/Close pause menu
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pauseMenuOpen)
            {
                OpenPauseMenu();
            } else
            {
                ClosePauseMenu();
            }
        }
    }
    //Make pause menu visible
    public void OpenPauseMenu()
    {
        int sceneIdx = FindFirstObjectByType<SceneLoader>().GetActiveSceneIdx();
        for (int i = 0; i < doNotOpenSceneIdx.Length; i++)
        {
            if (sceneIdx == doNotOpenSceneIdx[i])
            {
                return;
            }
        }

        pauseMenuCanvas.SetActive(true);
        pauseMenuOpen = true;
    }
    //Make pause menu invisible
    public void ClosePauseMenu()
    {
        pauseMenuCanvas.SetActive(false);
        pauseMenuOpen = false;
    }
    //Load main menu scene
    public void ToMainMenu()
    {
        ClosePauseMenu();
        FindFirstObjectByType<SceneLoader>().LoadMainMenu();
    }

}
