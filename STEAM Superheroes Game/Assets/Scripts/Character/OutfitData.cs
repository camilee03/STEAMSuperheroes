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
            string outfitName = gameObject.name.Substring(0, gameObject.name.Length-1);
            int outfitNum = int.Parse(gameObject.name[(gameObject.name.Length - 1)..]);
            OutfitManager.OutfitType outfitType = OutfitManager.OutfitType.Face;

            switch (outfitName)
            {
                case "face": outfitType = OutfitManager.OutfitType.Face; break;
                case "arms": outfitType = OutfitManager.OutfitType.Arms; break;
                case "pants": outfitType = OutfitManager.OutfitType.Pants; break;
                case "shirt": outfitType = OutfitManager.OutfitType.Shirts; break;
            }

            outfitManager.SetOutfit(outfitNum, outfitType);
        }
    }
    private void OnMouseExit()
    {
        transform.localScale = Vector3.one * initialSize;
    }
}
