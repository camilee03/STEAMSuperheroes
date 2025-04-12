using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static System.TimeZoneInfo;

public class LevelManager : MonoBehaviour
{
    bool[] unlockedLevels;

    [SerializeField] Texture2D[] levelImages;
    [SerializeField] RawImage currentLevel;
    int currentLevelIndex;

    private void Start()
    {
        unlockedLevels = new bool[SceneManager.sceneCountInBuildSettings]; // number of scenes in build
        for (int i = 0; i < unlockedLevels.Length; i++) // remove if unlocking mechanism exists
        {
            unlockedLevels[i] = true;
        }
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

    /// <summary> Load current gamestate </summary>
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

    /// <summary> Starts the game based on the current loaded level </summary>
    public void Play()
    {
        LoadScene(currentLevelIndex);
    }

    /// <summary> Loads Main Menu </summary>
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(2);
    }

    /// <summary> Loads Scene by index number</summary>
    public void LoadScene(int index)
    {
        if (unlockedLevels[currentLevelIndex]) { SceneManager.LoadScene(index); }
        else { Debug.Log("The current level is locked"); }
    }

    /// <summary> Determines what the current level to load is </summary>
    public void SetNewLevel(int level)
    {
        currentLevelIndex = (level + 2) * 2; // 4,6,8,10,12
        currentLevel.texture = levelImages[level];
    }

    /// <summary> Unlocks the level after the current level </summary>
    public void UnlockNextLevel()
    {
        unlockedLevels[SceneManager.GetActiveScene().buildIndex + 1] = true;
    }
}
