using UnityEngine;

public class Pickups : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            TriggerPickup();
            Destroy(gameObject);
        }
    }
    public virtual void TriggerPickup()
    {
        return;
    }
}
