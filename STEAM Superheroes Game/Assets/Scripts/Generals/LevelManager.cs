using System;
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

    [Header("Main Menu Specific")]
    [SerializeField] RawImage currentLevel;
    [SerializeField] TMP_Text levelLockedText;
    GameObject playButton;

    bool textFading;
    int finalLevelIndex = 14;
    int mainMenuIndex = 1;
    string currentLevelName = "";

    [Header("Locking Mechanisms")]
    [SerializeField] bool finalLevelUnlocked;

    public static LevelManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }


    private void Update()
    {
        if (Globals.Instance.gameLoaded && playButton == null && SceneManager.GetActiveScene().buildIndex == mainMenuIndex)
        {
            FindPlayButton();
        }
    }
    private void FindPlayButton()
    {
        playButton = GameObject.FindGameObjectWithTag("PlayButton");

        if (playButton == null) return;
        playButton.transform.GetChild(0).gameObject.SetActive(false);
        playButton.transform.GetChild(1).gameObject.SetActive(false);
    }


    #region Save/Load Data

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

    #endregion

    #region Scene Loading
    /// <summary> Loads Main Menu </summary>
    public void LoadMainMenu()
    {
        Debug.Log("Loading main menu " + "(mm index: " + mainMenuIndex + ")");
        SceneManager.LoadScene(mainMenuIndex);
    }

    /// <summary> Loads Scene by name </summary>
    public void LoadScene(string sceneName)
    {
        Debug.Log("Loading scene: " + sceneName + " by name");
        Scene scene = SceneManager.GetSceneByName(sceneName);

        if (scene.buildIndex != finalLevelIndex || CheckLockedLevels()) { SceneManager.LoadScene(sceneName); }
        else if (!textFading) { StartCoroutine(FadeText(levelLockedText)); }
    }

    /// <summary> Loads scene by its index </summary>
    public void LoadScene(int index)
    {
        Debug.Log("Loading scene: " + index + " by index");

        if (index != finalLevelIndex || CheckLockedLevels()) { SceneManager.LoadScene(index); }
        else if (!textFading) { StartCoroutine(FadeText(levelLockedText)); }
    }

    /// <summary> Load next scene in build order </summary>
    public void LoadNextScene()
    {
        Debug.Log("Loading next scene in build order");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary> Load same scene </summary>
    public void ReloadScene()
    {
        Debug.Log("Reloading Scene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public int GetActiveSceneIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    #endregion

    #region Main Menu

    /// <summary> Starts the game based on the current loaded level </summary>
    public void Play()
    {
        if (currentLevelName == "")
        {
            Debug.Log("No level selected");
            return;
        }

        LoadScene(currentLevelName);
    }

    /// <summary> Determines what the current level to load is </summary>
    public void SetNewLevel(int levelNumber)
    {
        if (playButton == null) playButton = GameObject.FindGameObjectWithTag("PlayButton");

        playButton.transform.GetChild(0).gameObject.SetActive(true);
        playButton.transform.GetChild(1).gameObject.SetActive(true);

        string levelName = "PreMinigame" + levelNumber;

        currentLevelName = levelName;
        if (currentLevel == null) currentLevel = GameObject.Find("CurrentLevel").GetComponent<RawImage>();

        currentLevel.texture = levelImages[levelNumber-1];

        Debug.Log(currentLevelName);
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

    #endregion

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
