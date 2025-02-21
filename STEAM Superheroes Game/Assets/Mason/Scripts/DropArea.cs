using System;
using System.Collections.Generic;
using UnityEngine;

public class DropArea : MonoBehaviour
{
    public Boolean correctPlacement = true;
    public List<GameObject> correctShapes;
    private List<GameObject> currentPlacedShapes;
    // void OnTriggerEnter2D(Collider2D collision)
    // {
    //     currentPlacedShapes.Add(collision.gameObject);
    // }
    // void OnTriggerExit2D(Collider2D collision)
    // {
    //     currentPlacedShapes.Remove(collision.gameObject);
    //     if(correctPlacement)
    //     {
    //         if(correctShapes.Contains(collision.gameObject))
    //         {
    //             correctPlacement = false;
    //         }
    //     }
    // }
    // void Update()
    // {
    //     if(!correctPlacement)
    //     {
    //         for(int i = 0; i < currentPlacedShapes.Count; i++)
    //         {
    //             if(correctShapes.Contains(currentPlacedShapes[i]))
    //             {
    //                 correctPlacement = true;
    //             }
    //         }
    //     }
    // }

    public bool getCorretPlacement()
    {
        return correctPlacement;
    }
}
