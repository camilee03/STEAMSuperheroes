using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    //Multiple methods to load scenes through SceneManager

    [SerializeField] int mainMenuIdx = 1;
    public static SceneLoader SceneLoaderInstance { get; private set; }
    //Create singleton
    private void Awake() {
        if (SceneLoaderInstance != null && SceneLoaderInstance != this) {
            Destroy(this.gameObject);
        } else {
            SceneLoaderInstance = this;
        }
        DontDestroyOnLoad(gameObject);
    }
    //Load scene with string name
    public void LoadSceneByName(string sceneName)
    {
        Debug.Log("Loading scene: " + sceneName + " by name");
        SceneManager.LoadScene(sceneName);
    }
    //Load scene with int index
    public void LoadSceneByIndex(int sceneIdx)
    {
        Debug.Log("Loading scene: " + sceneIdx + " by index");
        SceneManager.LoadScene(sceneIdx);
    }
    //Load next scene in build order
    public void LoadNextScene()
    {
        Debug.Log("Loading next scene in build order");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    //Load main menu, determined by mainMenuIdx variable
    public void LoadMainMenu()
    {
        Debug.Log("Loading main menu " + "(mm index: " + mainMenuIdx + ")");
        SceneManager.LoadScene(mainMenuIdx);
    }
    //Load same scene
    public void ReloadScene()
    {
        Debug.Log("Reloading Scene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    //Return scene index
    public int GetActiveSceneIdx()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }
}
