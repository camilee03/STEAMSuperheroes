using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HexBehavior : MonoBehaviour
{
    Color highlightedColor;
    Color opaqueColor;
    Material mat;

    private void Start()
    {
        mat = GetComponent<Renderer>().material;
        opaqueColor = mat.color;
        highlightedColor = Color.gray;
    }

    private void OnMouseEnter()
    {
        mat.color = highlightedColor;
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Destroy(gameObject);
        }
    }
    private void OnMouseExit()
    {
        mat.color = opaqueColor;
    }
}
