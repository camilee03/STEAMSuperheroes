using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static System.TimeZoneInfo;

public class LevelManager : MonoBehaviour
{

    [SerializeField] Texture2D[] levelImages;
    [SerializeField] RawImage currentLevel;
    [SerializeField] TMP_Text levelLockedText;
    bool textFading;
    int finalLevelIndex = 14;
    int mainMenuIndex = 2;
    int currentLevelIndex = 0;

    [Header("Locking Mechanisms")]
    [SerializeField] bool finalLevelUnlocked;

    /// <summary> Save current gamestate </summary>
    public void SaveGame()
    {
        PlayerPrefs.SetInt("score", Globals.Instance.score);
        PlayerPrefs.SetString("name", Globals.Instance.name);
        PlayerPrefs.SetInt("unlockedLevels", Globals.Instance.unlockedLevels);
        PlayerPrefs.SetInt("helmet", Globals.Instance.helmetNum);
        PlayerPrefs.SetInt("face", Globals.Instance.faceNum);
        PlayerPrefs.SetInt("shirt", Globals.Instance.shirtNum);
        PlayerPrefs.SetInt("arm", Globals.Instance.armNum);
        PlayerPrefs.SetInt("pants", Globals.Instance.pantsNum);

        string levelsCompleted = "";
        foreach (float level in Globals.Instance.levelsCompleted)
        {
            levelsCompleted += level + ",";
        }

        PlayerPrefs.SetString("levelsCompleted", levelsCompleted.Remove(-1));
    }

    /// <summary> Load current gamestate </summary>
    public void LoadGame()
    {
        Globals.Instance.score = PlayerPrefs.GetInt("score");
        Globals.Instance.name = PlayerPrefs.GetString("name");
        Globals.Instance.unlockedLevels = PlayerPrefs.GetInt("unlockedLevels");
        Globals.Instance.helmetNum = PlayerPrefs.GetInt("helmet");
        Globals.Instance.faceNum = PlayerPrefs.GetInt("face");
        Globals.Instance.shirtNum = PlayerPrefs.GetInt("shirt");
        Globals.Instance.armNum = PlayerPrefs.GetInt("arm");
        Globals.Instance.pantsNum = PlayerPrefs.GetInt("pants");

        string levelsCompleted = PlayerPrefs.GetString("levelsCompleted");
        foreach (string level in levelsCompleted.Split(','))
        {
            Globals.Instance.levelsCompleted.Add(float.Parse(level));
        }
    }

    /// <summary> Starts the game based on the current loaded level </summary>
    public void Play()
    {
        if (currentLevelIndex < 0) {
            Debug.Log("No level selected");
            return;
        }
        LoadScene(currentLevelIndex);
    }

    /// <summary> Loads Main Menu </summary>
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(mainMenuIndex);
    }

    /// <summary> Loads Scene by index number</summary>
    public void LoadScene(int index)
    {
        if (index != finalLevelIndex || CheckLockedLevels()) { SceneManager.LoadScene(index); }
        else if (!textFading) { StartCoroutine(FadeText(levelLockedText)); }
    }

    /// <summary> Determines what the current level to load is </summary>
    public void SetNewLevel(int level)
    {
        currentLevelIndex = (level + 2) * 2; // 4,6,8,10,12,14
        currentLevel.texture = levelImages[level];
    }

    /// <summary> Checks if final level is unlocked </summary>
    bool CheckLockedLevels()
    {
        if (finalLevelUnlocked) { return true; }

        float level1 = 1.1f;
        float level2 = 2.1f;
        float level3 = 3.1f;
        float level4 = 4.1f;
        float level5 = 5.1f;
        float[] levels = { level1, level2, level3, level4, level5 };

        foreach (float level in levels)
        {
            if (!Globals.Instance.levelsCompleted.Contains(level)) { return false; }
        }

        finalLevelUnlocked = true;
        return true;
    }

    IEnumerator FadeText(TMP_Text text) 
    {
        textFading = true;
        Color transparentColor = new Color(1, 0, 0, 0);
        Color opaqueColor = new Color(1, 0, 0, 1);
        Color currentColor = transparentColor;
        float change = 0.1f;
        float time = 0.1f;

        // Fade in
        while (currentColor.a < opaqueColor.a)
        {
            currentColor.a += change;
            text.color = currentColor;
            yield return new WaitForSeconds(time);
        }

        // Stay
        yield return new WaitForSeconds(2);

        // Fade out
        while (currentColor.a > transparentColor.a)
        {
            currentColor.a -= change;
            text.color = currentColor;
            yield return new WaitForSeconds(time);
        }

        textFading = false;
    }
}
