using UnityEngine;

public class LoadManager : MonoBehaviour
{
    void Start()
    {
        FindFirstObjectByType<SceneLoader>().LoadNextScene();
    }
}
