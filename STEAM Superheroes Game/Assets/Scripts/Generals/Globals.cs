using UnityEngine;

public class Globals : MonoBehaviour
{
    public int score;
    public int unlockedLevels;
    public int helmetNum;
    public int faceNum;
    public int shirtNum;
    public int armNum;
    public int pantsNum;


    public static Globals Instance { get; private set; }
    private void Awake()
    {
        // Creates an instance in the scene if there isn't one already
        if (Instance != null && Instance != this) { Destroy(this); }
        else { Instance = this; DontDestroyOnLoad(this); }
    }

}
