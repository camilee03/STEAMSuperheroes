using UnityEngine;
using UnityEngine.UI;

public class Grid : MonoBehaviour
{
    public DropArea[] dropAreas;
    public Text winText;
    private bool shouldWin = false;

    void Update()
    {
        int count = 0;
        for(int i = 0; i < dropAreas.Length; i++)
        {
            if(dropAreas[i].getCorretPlacement())
                count++;
        }
        if(count == dropAreas.Length)
        {
            shouldWin = true;
            winText.text = "YOU WIN";
        }
    }
}
