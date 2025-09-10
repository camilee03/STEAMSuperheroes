using UnityEngine;
using UnityEngine.Analytics;

public class ChooseEnum : MonoBehaviour
{
    public OutfitManager.OutfitType outfitType;
    [SerializeField] GameObject outfitManagerObject;
    OutfitManager outfitManager;

    private void Start()
    {
        outfitManager = outfitManagerObject.GetComponent<OutfitManager>();
    }

    public void CallChangeOutfitType()
    {
        outfitManager.ChangeOutfitType(outfitType);

        if (outfitType == OutfitManager.OutfitType.Face) { outfitManager.ChangeOutfitType(OutfitManager.OutfitType.Arms); }
        if (outfitType == OutfitManager.OutfitType.Arms) { outfitManager.ChangeOutfitType(OutfitManager.OutfitType.Face); }
    }
}
