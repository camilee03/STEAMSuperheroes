using Unity.VisualScripting;
using UnityEngine;

public class ShapeDragAndDrop : MonoBehaviour
{
    public bool isDragging = false;
    public Vector3 originalLocation;
    public Vector3 targetLocation;
    private Vector3 offset;
    public ShapeDragAndDrop selectedObject;
    public LayerMask layerMask;
    public LayerMask targetLayer;

    void Start()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.down, Mathf.Infinity, targetLayer);
        RaycastHit2D centerMostHit = FindCenterMostObject(hits, false);
        if (centerMostHit.collider != null)
        {
            transform.position = centerMostHit.transform.position;
            centerMostHit.collider.GetComponent<DropArea>().SetCurrentShape(this.gameObject);
        }
        this.originalLocation = this.transform.position;
    }

    void Update()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.down, Mathf.Infinity, targetLayer);
        if (Input.GetMouseButtonDown(0) && !isDragging)//Left Click
        {
            TrySelectObject();
            if (selectedObject == this)
            {
                RaycastHit2D centerMostHit = FindCenterMostObject(hits, true);
                centerMostHit.collider.GetComponent<DropArea>().RemoveCurrentShape();
            }
        }

        if (Input.GetMouseButtonUp(0) && isDragging)//Release
        {
            isDragging = false;
            selectedObject = null;

            RaycastHit2D centerMostHit = FindCenterMostObject(hits, false);
            if (centerMostHit.collider != null)
            {
                transform.position = centerMostHit.transform.position;
                centerMostHit.collider.GetComponent<DropArea>().SetCurrentShape(this.gameObject);
            }
            else
            {
                transform.position = originalLocation;
            }
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
        if (hit.collider != null && hit.collider.gameObject == gameObject && hit.collider.gameObject.CompareTag("Shape"))
        {
            isDragging = true;
            selectedObject = this;
            originalLocation = transform.position;
            offset = transform.position - GetMouseWorldPosition();
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Mathf.Abs(Camera.main.transform.position.z);
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    public RaycastHit2D FindCenterMostObject(RaycastHit2D[] hits, bool removingShape)
    {
        float minDistance = float.MaxValue;
        RaycastHit2D centerMostHit = new RaycastHit2D();

        foreach (RaycastHit2D hit in hits)
        {
            float distance = Vector2.Distance(hit.collider.transform.position, transform.position);
            if (distance < minDistance)
            {
                if(removingShape)
                {
                    minDistance = distance;
                    centerMostHit = hit;
                }
                else if (!hit.collider.GetComponent<DropArea>().occupied)
                {
                    minDistance = distance;
                    centerMostHit = hit;
                }
            }
        }

        return centerMostHit;
    }

}