using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager Instance { get; private set; }

    Dictionary<string, int> achievements = new Dictionary<string, int>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        // a few achievements are found done through here

    }

    void LoadAchievementsAndRewards()
    {

    }

    public void CompleteAchievement(string name)
    {
        achievements[name] = 1;
    }

    public bool CheckAchievement(string name)
    {
        return achievements.ContainsKey(name) && achievements[name] == 1;
    }
}
