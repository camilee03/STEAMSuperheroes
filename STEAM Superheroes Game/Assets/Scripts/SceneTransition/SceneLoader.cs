using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
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
        SceneManager.LoadScene(sceneName);
    }
    public void LoadSceneByIndex(int sceneIdx)
    {
        SceneManager.LoadScene(sceneIdx);
    }
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);

    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(1);

    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
