using UnityEngine;

public class PreMinigameSceneManager : MonoBehaviour
{
    public void Awake()
    {
        AudioManager.AudioManagerinstance.SetBGMTrack(BgmTrack.PREMINIGAME);
    }

    public void LoadGame() { //Assuming all pre minigames are followed by the minigame in sccene hierarchy
        LevelManager.Instance.LoadNextScene();
    }
    public void LoadMainMenu() { //Assuming all pre minigames are followed by the minigame in sccene hierarchy
        LevelManager.Instance.LoadMainMenu();
    }
}
