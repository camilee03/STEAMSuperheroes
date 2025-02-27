using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class OutfitManager : MonoBehaviour
{
    public Texture2D[] shirts;
    public Texture2D[] pants;
    public Texture2D[] face;
    public Texture2D[] arm;
    public Texture2D[] helmet;
    Texture2D[] currentTextures;

    public RawImage[] shirtImages;
    public RawImage[] pantImages;
    public RawImage[] faceImages;
    public RawImage[] armImages;
    public RawImage[] helmetImages;
    RawImage[] currentImages;

    [SerializeField] RawImage finalShirt;
    [SerializeField] RawImage finalPant;
    [SerializeField] RawImage finalFace;
    [SerializeField] RawImage finalArms;
    [SerializeField] RawImage finalHelmet;

    bool isPants;
    bool isShirts;
    bool isFace;
    bool isArms;


    // color scheme will be determined by %4 values

    public int colorScheme = 0; // determines what color the outfits are
    int colorVar = 0; // determines how many color variations there are

    private void Start()
    {
        ChangeOutfitType("pants");

        // put something here to set the outfit if the globals are different 
    }

    private void Update()
    {
        CycleThroughOutfits();
    }

    public void SetOutfit(int outfit)
    {
        int outfitNum = (outfit - 1) * currentImages.Length + colorScheme;

        if (isPants) 
        { 
            finalPant.texture = pantImages[outfit - 1].texture;
            Globals.Instance.pantsNum = outfitNum; 
        }
        if (isShirts) 
        { 
            finalShirt.texture = shirtImages[outfit - 1].texture;
            Globals.Instance.shirtNum = outfitNum;
        }
        if (isFace) 
        { 
            finalFace.texture = faceImages[outfit - 1].texture;
            Globals.Instance.faceNum = outfitNum;
        }
        if (isArms) 
        { 
            finalArms.texture = armImages[outfit - 1].texture;
            Globals.Instance.armNum = outfitNum;
        }
    }

    void CycleThroughOutfits()
    {
        for (int i = 0; i < currentImages.Length; i++)
        {
            currentImages[i].enabled = true;

            currentImages[i].texture = currentTextures[i * currentImages.Length + colorScheme];
        }
    }

    void DeactivateImages(RawImage[] imageList)
    {
        foreach (RawImage image in imageList)
        {
            image.enabled = false;
        }
    }

    public void ChangeOutfitType(string name)
    {
        isPants = false;
        isShirts = false;
        isArms = false;
        isFace = false;

        DeactivateImages(pantImages);
        DeactivateImages(shirtImages);
        DeactivateImages(faceImages);
        DeactivateImages(armImages);
        DeactivateImages(helmetImages);

        switch (name)
        {
            case "pants": isPants = true; currentImages = pantImages; currentTextures = pants; colorVar = 4; break;
            case "shirt": isShirts = true; currentImages = shirtImages; currentTextures = shirts; colorVar = 4; break;
            case "face": isFace = true; currentImages = faceImages; currentTextures = face; colorVar = 6; break;
            case "arms": isArms = true; currentImages = armImages; currentTextures = arm; colorVar = 6; break;
        }
    }

    public void ChangeColorScheme()
    {
        if (colorScheme < colorVar-1) { colorScheme++; }
        else { colorScheme = 0; }
    }
}
