using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    //This class handles all functions and methods for the pause menu

    [SerializeField] GameObject pauseMenuPopUp;
    [SerializeField] GameObject SoundSettingsMenu;
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

        ClosePauseMenu();
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
        int sceneIndex = LevelManager.Instance.GetActiveSceneIndex();
        for (int i = 0; i < doNotOpenSceneIdx.Length; i++)
        {
            if (sceneIndex == doNotOpenSceneIdx[i])
            {
                return;
            }
        }

        pauseMenuPopUp.SetActive(true);
        pauseMenuOpen = true;
    }
    //Make pause menu invisible
    public void ClosePauseMenu()
    {
        pauseMenuPopUp.SetActive(false);
        pauseMenuOpen = false;

        CloseSoundSettingsMenu();
    }
    //Load main menu scene
    public void ToMainMenu()
    {
        ClosePauseMenu();
        LevelManager.Instance.LoadMainMenu();
    }

    public void OpenSoundSettingsMenu()
    {
        // hide pause menu if it was open
        if(pauseMenuOpen) pauseMenuPopUp.SetActive(false);

        SoundSettingsMenu.SetActive(true);
    }

    public void CloseSoundSettingsMenu()
    {
        SoundSettingsMenu.SetActive(false);

        // reactivate pause menu if it was open
        if (pauseMenuOpen) pauseMenuPopUp.SetActive(true);
    }

}
