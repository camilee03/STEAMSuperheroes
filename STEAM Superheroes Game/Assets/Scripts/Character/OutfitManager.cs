using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class OutfitManager : MonoBehaviour
{
    [Header("Types of Images")]
    public Texture2D[] shirts;
    public Texture2D[] pants;
    public Texture2D[] face;
    public Texture2D[] arm;
    public Texture2D[] helmet;
    Texture2D[] currentTextures;

    // Final UI Image
    [Header("Final Images")]
    [SerializeField] RawImage finalShirt;
    [SerializeField] RawImage finalPant;
    [SerializeField] RawImage finalFace;
    [SerializeField] RawImage finalArms;
    [SerializeField] RawImage finalHelmet;

    // Images
    [Header("Image Choices")]
    [SerializeField] RawImage[] shirtImages;
    [SerializeField] RawImage[] pantsImages;
    [SerializeField] RawImage[] faceImages;
    [SerializeField] RawImage[] armImages;
    RawImage[] currentImages;

    // GameObject that holds image options
    [Header("GameObjects")]
    [SerializeField] GameObject shirtObject;
    [SerializeField] GameObject pantObject;
    [SerializeField] GameObject faceAndArmObject;
    //[SerializeField] GameObject helmetObject;
    GameObject currentObject;

    // What current selection item is
    bool isPants;
    bool isShirts;
    bool isFace;
    bool isArms;
    int helmetNum = 0;

    int skinColor;

    // color scheme will be determined by %4 values

    int colorScheme = 0; // determines what color the outfits are
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
        SetOutfit(Random.Range(1, 4), "face");

        SetOutfit(Random.Range(1, 3), "arms");

        // Randomize clothes
        int newColor = Random.Range(0, 3);
        colorScheme = newColor;
        SetOutfit(Random.Range(1, 5), "shirt");

        colorScheme = newColor;
        SetOutfit(Random.Range(1, 5), "pants");

        int changeHelmet = Random.Range(0, 2);
        if (changeHelmet == 1) { ChangeOutfitType("helmet"); }

        ChangeOutfitType(currentOutfitType);
        colorScheme = initialColorScheme;
        skinColor = initialSkinColor;
    }

    public void SetOutfit(int outfit, string name)
    {
        ChangeOutfitType(name);

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
            currentImages[i].texture = currentTextures[i * colorVar + colorScheme]; 
        }

        currentObject.SetActive(true);
    }

    public void ChangeOutfitType(string name)
    {
        if (name != "helmet")
        {
            isPants = false;
            isShirts = false;
            isArms = false;
            isFace = false;

            if (currentObject != null) currentObject.SetActive(false);

            switch (name)
            {
                case "pants": isPants = true; currentImages = pantsImages;  currentObject = pantObject; currentTextures = pants; colorVar = 4; colorScheme = 0; break;
                case "shirt": isShirts = true; currentImages = shirtImages;  currentObject = shirtObject; currentTextures = shirts; colorVar = 4; colorScheme = 0; break;
                case "face": isFace = true; currentImages = faceImages; currentObject = faceAndArmObject; currentTextures = face; colorVar = 6; colorScheme = skinColor; break;
                case "arms": isArms = true; currentImages = armImages; currentObject = faceAndArmObject; currentTextures = arm; colorVar = 6; colorScheme = skinColor; break;
            }
        }
        else { SwitchHelmet(); }
    }

    public void ChangeColorScheme(int newColors)
    {
        colorScheme = newColors;
    }
}
