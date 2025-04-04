using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] int mainMenuIdx = 1;
    public static SceneLoader SceneLoaderInstance { get; private set; }
    private void Awake() {
        if (SceneLoaderInstance != null && SceneLoaderInstance != this) {
            Destroy(this);
        } else {
            SceneLoaderInstance = this;
        }
        DontDestroyOnLoad(gameObject);
    }
    public void LoadSceneByName(string sceneName)
    {
        Debug.Log("Loading scene: " + sceneName + " by name");
        SceneManager.LoadScene(sceneName);
    }
    public void LoadSceneByIndex(int sceneIdx)
    {
        Debug.Log("Loading scene: " + sceneIdx + " by index");
        SceneManager.LoadScene(sceneIdx);
    }
    public void LoadNextScene()
    {
        Debug.Log("Loading next scene in build order");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    public void LoadMainMenu()
    {
        Debug.Log("Loading main menu " + "(mm index: " + mainMenuIdx + ")");
        SceneManager.LoadScene(mainMenuIdx);
    }
    public void ReloadScene()
    {
        Debug.Log("Reloading Scene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public int GetActiveSceneIdx()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }
}
