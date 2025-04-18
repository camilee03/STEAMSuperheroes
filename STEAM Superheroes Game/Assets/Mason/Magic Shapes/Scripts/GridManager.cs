using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    public DropArea[] importantDropAreas;
    [SerializeField] WinLevel winLevel;
    bool levelWon = false;

    void Update()
    {
        int count = 0;
        foreach (DropArea dropArea in importantDropAreas)
        {
            if (dropArea.correctPlacement)
            {
                count++;
            }
        }
        bool allShapesDropped = true;

        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Shape");
        foreach (GameObject obj in gameObjects)
        {
            ShapeDragAndDrop shape = obj.GetComponent<ShapeDragAndDrop>();
            if(shape.isDragging)
            {
                allShapesDropped = false;
            }
        }

        if (allShapesDropped && count == importantDropAreas.Length)
        {
            if (!levelWon) {
                levelWon = true;
                winLevel.ActivateCanvas();
            }
        }
    }
}
