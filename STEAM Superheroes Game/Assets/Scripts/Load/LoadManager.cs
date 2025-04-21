using UnityEngine;

public class LoadManager : MonoBehaviour
{
    //This class should only be placed in load scene and simply moves to the next scene
    void Start()
    {
        FindFirstObjectByType<SceneLoader>().LoadNextScene();
    }
}
