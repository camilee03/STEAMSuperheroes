using UnityEngine;

public class Pickup_Satellite : Pickups
{
    public override void TriggerPickup()
    {
        FindFirstObjectByType<ProgrammingMinigameManager>().IncreasePiecesCollected(gameObject);
    }
}
