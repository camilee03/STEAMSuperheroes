using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class MineralMixer : MonoBehaviour
{
    [Header("Local Position Values")]
    [SerializeField] float minY;
    [SerializeField] float maxY;
    [SerializeField] float minX;
    [SerializeField] float maxX;

    List<Vector3> takenPositions = new List<Vector3>();


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).localPosition = GetRandomPosition();
        }
    }

    Vector3 GetRandomPosition()
    {
        float currentY = Random.Range(minY, maxY);
        float currentX = Random.Range(minX, maxX);

        Vector3 newPosition = new Vector3(currentX, currentY);

        foreach (Vector3 pos in takenPositions)
        {
            if (Vector3.Distance(pos, newPosition) < 0.5f)
            {
                newPosition = GetRandomPosition();
            }
        }

        takenPositions.Add(newPosition);
        return newPosition;
    }

}
