using UnityEngine;

public class Pickup_Satellite : Pickups
{
    protected override void TriggerPickup()
    {
        FindFirstObjectByType<ProgrammingMinigameManager>().IncreasePiecesCollected(gameObject);
    }
}
