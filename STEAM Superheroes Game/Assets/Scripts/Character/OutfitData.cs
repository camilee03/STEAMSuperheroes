using UnityEngine;
using UnityEngine.InputSystem;

public class OutfitData : MonoBehaviour
{
    int size = 20;
    OutfitManager outfitManager;

    private void Start()
    {
        outfitManager = GetComponentInParent<OutfitManager>();
    }

    private void Update()
    {
        transform.localScale = Vector3.one * size;
    }
    private void OnMouseEnter()
    {
        size = 30;
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
        size = 20;
    }
}
