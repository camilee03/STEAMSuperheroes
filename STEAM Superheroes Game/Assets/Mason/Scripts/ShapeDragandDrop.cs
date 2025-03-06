using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShapeDragandDrop : MonoBehaviour
{
    protected bool isDragging = false;
    private Vector3 offset;
    private Vector3 startingLocation;
    private Vector3 dropArea;
    private static ShapeDragandDrop selectedObject;
    public LayerMask layerMask;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left-click
        {
            TrySelectObject();
        }

        if (Input.GetMouseButtonUp(0)) // Release
        {
            isDragging = false;
            selectedObject = null;
        }

        if (isDragging && selectedObject == this)
        {
            transform.position = GetMouseWorldPosition() + offset;
        }
    }

    private void TrySelectObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, layerMask);

        if (hit.collider != null && hit.collider.gameObject == gameObject)
        {
            isDragging = true;
            selectedObject = this;
            offset = transform.position - GetMouseWorldPosition();
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Mathf.Abs(Camera.main.transform.position.z); // Ensure correct depth
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

}