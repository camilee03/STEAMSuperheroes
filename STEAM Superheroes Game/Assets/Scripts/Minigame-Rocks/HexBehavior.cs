using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HexBehavior : MonoBehaviour
{
    Color highlightedColor;
    Color opaqueColor;
    Material mat;
    MineralManager mineralManager;

    private void Start()
    {
        mat = GetComponent<Renderer>().material;
        mineralManager = GameObject.Find("MineralManager").GetComponent<MineralManager>();
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
            mineralManager.RemoveClick();
            Destroy(gameObject);
        }
    }
    private void OnMouseExit()
    {
        mat.color = opaqueColor;
    }
}
