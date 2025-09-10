using System;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using static OutfitManager;
using Random = UnityEngine.Random;

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
    [Serializable]
    public enum OutfitType { Pants, Shirts, Face, Arms, Helmet };

    OutfitType currentOutfit;

    int helmetNum = 0;

    int skinColor;

    // color scheme will be determined by %4 values

    int colorScheme = 0; // determines what color the outfits are
    int colorVar = 0; // determines how many color variations there are

    private void Start()
    {
        ChangeOutfitType(OutfitType.Face);
        ChangeOutfitType(OutfitType.Arms);

        // sets the outfit if the globals are different 
        finalShirt.texture = shirts[Globals.Instance.shirtNum];
        finalPant.texture = pants[Globals.Instance.pantsNum];
        finalFace.texture = face[Globals.Instance.faceNum];
        finalArms.texture = arm[Globals.Instance.armNum];
        finalHelmet.texture = helmet[Globals.Instance.helmetNum];
        skinColor = Globals.Instance.faceNum;
    }

    public void RandomizeOutfit()
    {
        // Store current values
        int initialColorScheme = colorScheme;
        int initialSkinColor = skinColor;
        OutfitType currentOutfitType = currentOutfit;

        // Randomize skin
        colorScheme = Random.Range(0, 5);
        skinColor = colorScheme;
        SetOutfit(Random.Range(1, 4), OutfitType.Face);

        SetOutfit(Random.Range(1, 3), OutfitType.Arms);

        // Randomize clothes
        int newColor = Random.Range(0, 3);
        colorScheme = newColor;
        SetOutfit(Random.Range(1, 5), OutfitType.Shirts);

        colorScheme = newColor;
        SetOutfit(Random.Range(1, 5), OutfitType.Pants);

        int changeHelmet = Random.Range(0, 2);
        if (changeHelmet == 1) { ChangeOutfitType(OutfitType.Helmet); }

        ChangeOutfitType(currentOutfitType);
        colorScheme = initialColorScheme;
        skinColor = initialSkinColor;
    }

    public void SetOutfit(int outfit, OutfitType name)
    {
        ChangeOutfitType(name);

        int outfitNum = (outfit - 1) * colorVar + colorScheme;

        switch (currentOutfit)
        {
            case OutfitType.Pants:
                finalPant.texture = pants[outfitNum];
                Globals.Instance.pantsNum = outfitNum;
                break;

            case OutfitType.Shirts:
                finalShirt.texture = shirts[outfitNum];
                Globals.Instance.shirtNum = outfitNum;
                break;

            case OutfitType.Face:
                finalFace.texture = face[outfitNum];
                Globals.Instance.faceNum = outfitNum;

                // 0-5 (o0) 6-11 (o1)

                // Also update the arms

                int armOutfit = 0; 
                if (Globals.Instance.armNum > 5)
                {
                    armOutfit = 6;
                }
                Debug.Log($"Arm Num: {Globals.Instance.armNum} Arm Outfit: {armOutfit} Color Scheme {colorScheme}");
                finalArms.texture = arm[armOutfit + colorScheme];
                Globals.Instance.armNum = armOutfit + colorScheme;

                skinColor = colorScheme;
                break;

            case OutfitType.Arms:
                finalArms.texture = arm[outfitNum];
                Globals.Instance.armNum = outfitNum;

                // Also update the face
                int faceOutfit = 0;
                if (Globals.Instance.faceNum > 11) 
                {
                    faceOutfit = 12;
                }
                else if (Globals.Instance.faceNum > 5)
                {
                    faceOutfit = 6;
                }
                Debug.Log($"Face Num: {Globals.Instance.faceNum} Face Outfit: {faceOutfit} Color Scheme {colorScheme}");
                finalFace.texture = face[faceOutfit + colorScheme];
                Globals.Instance.faceNum = faceOutfit + colorScheme;

                skinColor = colorScheme;
                break;
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
    }

    public void ChangeOutfitType(OutfitType newOutfit)
    {
        if (newOutfit == currentOutfit) { CycleThroughOutfits(); return; }

        if (newOutfit != OutfitType.Helmet)
        {
            shirtObject.SetActive(false);
            pantObject.SetActive(false);
            faceAndArmObject.SetActive(false);

            currentOutfit = newOutfit;

            switch (newOutfit)
            {
                case OutfitType.Pants: currentImages = pantsImages;  currentObject = pantObject; currentTextures = pants; colorVar = 4; colorScheme = 0; break;
                case OutfitType.Shirts: currentImages = shirtImages;  currentObject = shirtObject; currentTextures = shirts; colorVar = 4; colorScheme = 0; break;
                case OutfitType.Face: currentImages = faceImages; currentObject = faceAndArmObject; currentTextures = face; colorVar = 6; colorScheme = skinColor; break;
                case OutfitType.Arms: currentImages = armImages; currentObject = faceAndArmObject; currentTextures = arm; colorVar = 6; colorScheme = skinColor; break;
            }

            currentObject.SetActive(true);
        }
        else { SwitchHelmet(); }

        CycleThroughOutfits();
    }

    public void ChangeColorScheme(int newColors)
    {
        colorScheme = newColors;
        CycleThroughOutfits();
        if (currentOutfit == OutfitType.Face || currentOutfit == OutfitType.Arms) { skinColor = colorScheme; }

        if (currentOutfit == OutfitType.Face) { ChangeOutfitType(OutfitType.Arms); }
        if (currentOutfit == OutfitType.Arms) { ChangeOutfitType(OutfitType.Face); }
    }
}
