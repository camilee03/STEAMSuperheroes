using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class OutfitData : MonoBehaviour
{
    int size = 20;
    int initialSize;
    OutfitManager outfitManager;
    Color transparentColor;
    Color opaqueColor;
    Material mat;

    private void Start()
    {
        outfitManager = GetComponentInParent<OutfitManager>();
        mat = GetComponent<Renderer>().material;
        opaqueColor = mat.color;
        transparentColor = Color.clear;
        initialSize = (int)transform.localScale.x;
        size = initialSize;
    }

    private void Update()
    {
        transform.localScale = Vector3.one * size;

        CheckIfEnabled();
    }
    private void CheckIfEnabled()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<RawImage>().enabled)
            {
                mat.color = opaqueColor;
                return;
            }
        }

        mat.color = transparentColor;
    }

    private void OnMouseEnter()
    {
        size = initialSize + 10;
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            outfitManager.SetOutfit(int.Parse(gameObject.name));
        }
    }
    private void OnMouseExit()
    {
        size = initialSize;
    }
}
