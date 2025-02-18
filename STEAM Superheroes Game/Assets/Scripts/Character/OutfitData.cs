using UnityEngine;

public class OutfitData : MonoBehaviour
{
    int size = 20;
    private void Update()
    {
        transform.localScale = Vector3.one * size;
    }
    private void OnMouseEnter()
    {
        Debug.Log("Enter");
        size = 30;
    }
    private void OnMouseExit()
    {
        size = 20;
    }
}
