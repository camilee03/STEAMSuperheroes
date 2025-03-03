using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static System.TimeZoneInfo;

public class LevelManager : MonoBehaviour
{
    bool[] unlockedLevels;

    public float transitionTime = 0.2f;

    public Vector3 tempStart;

    [SerializeField] private GameObject _playerPrefab;

    private void Start()
    {
        unlockedLevels = new bool[UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings]; // number of scenes in build
        //for (int i = 0; i < unlockedLevels.Length; i++) // remove if unlocking mechanism exists
        //{
        //    unlockedLevels[i] = true;
        //}
    }

    /// <summary> Save current gamestate </summary>
    public void SaveGame()
    {
        PlayerPrefs.SetInt("score", Globals.Instance.score);
        PlayerPrefs.SetInt("unlockedLevels", Globals.Instance.unlockedLevels);
        PlayerPrefs.SetInt("helmet", Globals.Instance.helmetNum);
        PlayerPrefs.SetInt("face", Globals.Instance.faceNum);
        PlayerPrefs.SetInt("shirt", Globals.Instance.shirtNum);
        PlayerPrefs.SetInt("arm", Globals.Instance.armNum);
        PlayerPrefs.SetInt("pants", Globals.Instance.pantsNum);
    }

    public void LoadGame()
    {
        Globals.Instance.score = PlayerPrefs.GetInt("score");
        Globals.Instance.unlockedLevels = PlayerPrefs.GetInt("unlockedLevels");
        Globals.Instance.helmetNum = PlayerPrefs.GetInt("helmet");
        Globals.Instance.faceNum = PlayerPrefs.GetInt("face");
        Globals.Instance.shirtNum = PlayerPrefs.GetInt("shirt");
        Globals.Instance.armNum = PlayerPrefs.GetInt("arm");
        Globals.Instance.pantsNum = PlayerPrefs.GetInt("pants");
    }

    /// <summary> Loads next level if unlocked </summary>
    public void LoadNextLevel(Vector3 startPosition)
    {
        if (unlockedLevels[SceneManager.GetActiveScene().buildIndex])
        {
            StartCoroutine(ChangeScene(SceneManager.GetActiveScene().buildIndex + 1));
        }
        else { Debug.LogError("Level hasn't been unlocked"); }
    }

    /// <summary> Load scene by specific name </summary>
    public void LoadSceneByName(string sceneName, Vector3 startPosition)
    {
        int buildIndex = SceneUtility.GetBuildIndexByScenePath(sceneName);
        //Scene loadScene = SceneManager.GetSceneByName(sceneName);

        if (buildIndex == -1) { Debug.LogWarning("Scene '" + sceneName + "' not found in Build Settings."); }
        else if (unlockedLevels[buildIndex]) { StartCoroutine(ChangeScene(buildIndex)); }
        else { Debug.Log("The current level is locked"); }
    }

    /// <summary> Unlocks the level after the current level </summary>
    public void UnlockNextLevel()
    {
        unlockedLevels[SceneManager.GetActiveScene().buildIndex + 1] = true;
    }

    /// <summary> Change Scene coroutine </summary>
    IEnumerator ChangeScene(int sceneIndex)
    {
        yield return new WaitForSeconds(transitionTime);
        AsyncOperation aSync = SceneManager.LoadSceneAsync(sceneIndex);

        while (!aSync.isDone)
        {
            yield return null;
        }
    }
}
