using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class OutfitData : MonoBehaviour
{
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
            string outfitType = gameObject.name.Substring(0, gameObject.name.Length-2);
            outfitManager.SetOutfit(int.Parse(gameObject.name[(gameObject.name.Length - 1)..]), outfitType);
        }
    }
    private void OnMouseExit()
    {
        transform.localScale = Vector3.one * initialSize;
    }
}
