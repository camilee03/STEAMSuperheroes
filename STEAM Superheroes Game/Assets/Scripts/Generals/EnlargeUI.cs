using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class EnlargeUI : MonoBehaviour
{
    int initialSize;

    private void Start()
    {
        initialSize = (int)transform.localScale.x;
    }

    private void OnMouseEnter()
    {
        transform.localScale = Vector3.one * (initialSize + 10);
        Debug.Log("HERE");
    }

    private void OnMouseExit()
    {
        transform.localScale = Vector3.one * initialSize;
    }
}
