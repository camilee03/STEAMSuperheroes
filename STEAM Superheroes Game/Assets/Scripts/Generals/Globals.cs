using UnityEngine;
using System.Collections.Generic;

public class Globals : MonoBehaviour
{

    public static Globals Instance { get; private set; }
    private void Awake()
    {
        // Creates an instance in the scene if there isn't one already
        if (Instance != null && Instance != this) { Destroy(this); }
        else { Instance = this; DontDestroyOnLoad(this); }
    }

    public int score = 0;
    public int unlockedLevels = 0;
    public int helmetNum = 0;
    public int faceNum = 4;
    public int shirtNum = 0;
    public int armNum = 4;
    public int pantsNum = 0;

    public List<float> levelsCompleted = new List<float>();
}
