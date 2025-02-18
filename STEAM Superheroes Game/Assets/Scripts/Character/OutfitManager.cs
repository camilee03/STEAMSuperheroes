using UnityEngine;
using UnityEngine.UI;

public class OutfitManager : MonoBehaviour
{
    public Texture2D[] shirts;
    public Texture2D[] pants;

    public RawImage[] shirtImages;
    public RawImage[] pantImages;

    // color scheme will be determined by %4 values

    public int colorScheme = 0;
    int pantType = 0;
    int shirtType = 0;

    private void Update()
    {
        for (int i = 0; i < pantImages.Length; i++)
        {
            pantImages[i].texture = pants[i * 4 + colorScheme];
        }
    }

    void CycleThroughShirts()
    {

    }

    void CycleThroughPants()
    {

    }

    public void ChangeColorScheme()
    {
        if (colorScheme < 3) { colorScheme++; }
        else { colorScheme = 0; }
    }
}
