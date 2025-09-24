using UnityEngine;

public class SettingsMenuButton : MonoBehaviour
{
    private PauseMenu pauseMenu;

    private void Start()
    {
        pauseMenu = GameObject.FindFirstObjectByType<PauseMenu>();
    }

    public void OpenSettingsMenu()
    {
        if (pauseMenu == null) GameObject.FindFirstObjectByType<PauseMenu>().OpenPauseMenu();

        pauseMenu.OpenPauseMenu();
    }
}
