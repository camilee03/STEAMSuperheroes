using UnityEngine;

public class SettingsMenuButton : MonoBehaviour
{
    public void OpenSettingsMenu() => GameObject.FindFirstObjectByType<PauseMenu>().OpenSoundSettingsMenu();
}
