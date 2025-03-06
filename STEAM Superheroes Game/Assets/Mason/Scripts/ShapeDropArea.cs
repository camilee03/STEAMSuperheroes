using UnityEngine;

public class ShapeDropArea : ShapeDragandDrop
{
    private Collider2D targetLocation;
    public Vector3 location;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "DropArea")
        {
            targetLocation = collision;
        }
    }

    void Update()
    {
        if (!isDragging && targetLocation != null)
        {
            this.transform.parent.gameObject.transform.position = targetLocation.gameObject.transform.position;
        }
    }
}
