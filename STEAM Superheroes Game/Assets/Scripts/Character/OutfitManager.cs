using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class OutfitManager : MonoBehaviour
{
    // Images of shirts that can exist
    public Texture2D[] shirts;
    public Texture2D[] pants;
    public Texture2D[] face;
    public Texture2D[] arm;
    public Texture2D[] helmet;
    Texture2D[] currentTextures;

    // UI Images that can be switched between
    public RawImage[] shirtImages;
    public RawImage[] pantImages;
    public RawImage[] faceImages;
    public RawImage[] armImages;
    RawImage[] currentImages;

    // Final UI Image
    [SerializeField] RawImage finalShirt;
    [SerializeField] RawImage finalPant;
    [SerializeField] RawImage finalFace;
    [SerializeField] RawImage finalArms;
    [SerializeField] RawImage finalHelmet;

    // What current selection item is
    bool isPants;
    bool isShirts;
    bool isFace;
    bool isArms;
    int helmetNum = 0;

    int skinColor;

    // color scheme will be determined by %4 values

    public int colorScheme = 0; // determines what color the outfits are
    int colorVar = 0; // determines how many color variations there are

    private void Start()
    {
        ChangeOutfitType("face");

        // sets the outfit if the globals are different 
        finalShirt.texture = shirts[Globals.Instance.shirtNum];
        finalPant.texture = pants[Globals.Instance.pantsNum];
        finalFace.texture = face[Globals.Instance.faceNum];
        finalArms.texture = arm[Globals.Instance.armNum];
        finalHelmet.texture = helmet[Globals.Instance.helmetNum];
        skinColor = Globals.Instance.faceNum;
    }

    private void Update()
    {
        CycleThroughOutfits();
    }

    public void RandomizeOutfit()
    {
        int initialColorScheme = colorScheme;
        int initialSkinColor = skinColor;
        string currentOutfitType = "face";
        if (isArms) { currentOutfitType = "arms"; }
        if (isShirts) { currentOutfitType = "shirt"; }
        if (isPants) { currentOutfitType = "pants"; }

        // Randomize skin
        colorScheme = Random.Range(0, 5);
        skinColor = colorScheme;
        ChangeOutfitType("face");
        SetOutfit(Random.Range(1, 4));

        ChangeOutfitType("arms");
        SetOutfit(Random.Range(1, 3));

        // Randomize clothes
        colorScheme = Random.Range(0, 3);
        ChangeOutfitType("shirt");
        SetOutfit(Random.Range(1, 5));

        ChangeOutfitType("pants");
        SetOutfit(Random.Range(1, 5));

        int changeHelmet = Random.Range(0, 2);
        if (changeHelmet == 1) { ChangeOutfitType("helmet"); }

        colorScheme = initialColorScheme;
        skinColor = initialSkinColor;
        ChangeOutfitType(currentOutfitType);
    }

    public void SetOutfit(int outfit)
    {
        int outfitNum = (outfit - 1) * colorVar + colorScheme;

        if (isPants)
        {
            finalPant.texture = pants[outfitNum];
            Globals.Instance.pantsNum = outfitNum;
        }
        if (isShirts)
        {
            finalShirt.texture = shirts[outfitNum];
            Globals.Instance.shirtNum = outfitNum;
        }

        if (isFace)
        {
            finalFace.texture = face[outfitNum];
            Globals.Instance.faceNum = outfitNum;

            if (skinColor == -1)
            {
                finalArms.texture = arm[colorScheme];
                Globals.Instance.armNum = colorScheme;
            }
            else
            {
                int armOutfit = 0;
                if (Globals.Instance.armNum >= 0) { armOutfit = Globals.Instance.armNum - skinColor; }

                finalArms.texture = arm[armOutfit + colorScheme];
                Globals.Instance.armNum = armOutfit + colorScheme;
            }

            skinColor = colorScheme;
        }
        if (isArms)
        {
            finalArms.texture = arm[outfitNum];
            Globals.Instance.armNum = outfitNum;

            if (skinColor == -1)
            {
                finalFace.texture = face[colorScheme];
                Globals.Instance.faceNum = colorScheme;
            }
            else
            {
                int faceOutfit = 0;
                if (Globals.Instance.faceNum >= 0) { faceOutfit = Globals.Instance.faceNum - skinColor; }

                finalFace.texture = face[faceOutfit + colorScheme];
                Globals.Instance.faceNum = faceOutfit + colorScheme;
            }

            skinColor = colorScheme;
        }
    }

    public void SwitchHelmet()
    {
        if (helmetNum < 1) { helmetNum++; }
        else { helmetNum = 0; }
        finalHelmet.texture = helmet[helmetNum];

        Globals.Instance.helmetNum = helmetNum;
    }

    void CycleThroughOutfits()
    {
        for (int i = 0; i < currentImages.Length; i++)
        {
            currentImages[i].enabled = true;

            currentImages[i].texture = currentTextures[i * colorVar + colorScheme]; 
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
        if (name != "helmet")
        {
            isPants = false;
            isShirts = false;
            isArms = false;
            isFace = false;

            DeactivateImages(pantImages);
            DeactivateImages(shirtImages);
            DeactivateImages(faceImages);
            DeactivateImages(armImages);

            switch (name)
            {
                case "pants": isPants = true; currentImages = pantImages; currentTextures = pants; colorVar = 4; colorScheme = 0; break;
                case "shirt": isShirts = true; currentImages = shirtImages; currentTextures = shirts; colorVar = 4; colorScheme = 0; break;
                case "face": isFace = true; currentImages = faceImages; currentTextures = face; colorVar = 6; colorScheme = skinColor; break;
                case "arms": isArms = true; currentImages = armImages; currentTextures = arm; colorVar = 6; colorScheme = skinColor; break;
            }
        }
        else { SwitchHelmet(); }
    }

    public void ChangeColorScheme(int newColors)
    {
        colorScheme = newColors;
    }
}
