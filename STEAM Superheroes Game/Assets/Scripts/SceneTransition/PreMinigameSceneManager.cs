using UnityEngine;

public class PreMinigameSceneManager : MonoBehaviour
{
    public void LoadGame() { //Assuming all pre minigames are followed by the minigame in sccene hierarchy
        FindFirstObjectByType<SceneLoader>().LoadNextScene();
    }
}
