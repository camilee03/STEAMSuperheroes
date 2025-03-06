using UnityEngine;

public class DisableRenderOnStart : MonoBehaviour
{
    void Start()
    {
        GetComponent<Renderer>().enabled = false;
    }
}
