using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class OutfitData : MonoBehaviour
{
    int size = 20;
    int initialSize;
    [SerializeField] OutfitManager outfitManager;

    private void Start()
    {
        initialSize = (int)transform.localScale.x;
    }

    private void OnMouseEnter()
    {
        transform.localScale = Vector3.one * (initialSize + 10);
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
        transform.localScale = Vector3.one * initialSize;
    }
}
