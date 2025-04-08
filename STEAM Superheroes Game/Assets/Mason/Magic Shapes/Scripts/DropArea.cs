using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DropArea : MonoBehaviour
{
    public bool correctPlacement = true;
    public List<GameObject> correctShapes;
    public bool occupied = false;
    public GameObject currentShape;

    void Awake()
    {
        currentShape = null;
    }

    public void SetCurrentShape(GameObject shape)
    {
        occupied = true;
        currentShape = shape;
        foreach (GameObject gameObject in correctShapes)
        {
            if (gameObject == currentShape)
            {
                correctPlacement = true;
                Debug.Log(this.gameObject.name + " ST");
            }
        }
    }

    public void RemoveCurrentShape()
    {
        occupied = false;
        currentShape = null;
        correctPlacement = false;
        Debug.Log(this.gameObject.name + " RM");
    }

    void Update()
    {
        if (correctShapes.Count == 0 && !occupied)
        {
            correctPlacement = true;
        }
        else if (correctShapes.Count == 0 && occupied)
        {
            correctPlacement = false;
        }
        else if (correctShapes.Count > 0 && !occupied)
        {
            correctPlacement = false;
        }
    }
}




// void OnTriggerEnter2D(Collider2D collision)
//     {
//         if(!occupied)
//         {
//             Debug.Log("T1");
//             if(collision.CompareTag("HitBox"))
//             {
//                 Debug.Log("T2");
//                 GameObject parent = collision.transform.parent.gameObject;
//                 if (parent.GetComponent<ShapeDragAndDrop>() != null && !parent.GetComponent<ShapeDragAndDrop>().isDragging)
//                 {
//                     Debug.Log("T3");
//                     currentShape = parent;
//                     occupied = true;
//                     if (correctShapes.Count > 0)//Has more than 1 item in it
//                     {
//                         foreach(GameObject obj in correctShapes)
//                         {
//                             if(obj == currentShape)
//                             {
//                                 correctPlacement = true;
//                             }
//                         }
//                     }
//                     else
//                     {
//                         correctPlacement = false;
//                     }
//                 }
//             }
//         }
//     }
//     void OnTriggerExit2D(Collider2D collision)
//     {
//         if (collision.transform.parent.gameObject == currentShape)
//         {
//             currentShape = null;
//             occupied = false;
//         }
//     }