using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenuCanvas = null;
    [SerializeField] int[] doNotOpenSceneIdx = null;
    bool pauseMenuOpen = false;
    public static PauseMenu PauseMenuInstance { get; private set; }
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
    public void ClosePauseMenu()
    {
        pauseMenuCanvas.SetActive(false);
        pauseMenuOpen = false;
    }
    public void ToMainMenu()
    {
        ClosePauseMenu();
        FindFirstObjectByType<SceneLoader>().LoadMainMenu();
    }

}
