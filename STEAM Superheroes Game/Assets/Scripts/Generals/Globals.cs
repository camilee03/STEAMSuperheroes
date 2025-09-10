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

    // JSON Values -- To be updated & saved OnSave()
    public int score = 0;
    public int unlockedLevels = 0;
    public int helmetNum = 0;
    public int faceNum = 4;
    public int shirtNum = 0;
    public int armNum = 4;
    public int pantsNum = 0;
    public string superheroName = "";

    public List<float> levelsCompleted = new List<float>();

    // Other values -- To be used in general housekeeping
    public bool gameLoaded = false;
}
