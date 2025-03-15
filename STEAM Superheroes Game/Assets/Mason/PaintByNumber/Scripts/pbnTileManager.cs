using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class pbnTileManager : MonoBehaviour
{
    private Tilemap tilemap;
    public TileBase selectedTile;
    public TileBase outlineTile;
    [SerializeField] Text winText;
    [SerializeField] List<Vector3Int> checkPositions;
    [SerializeField] List<TileBase> correctTiles;
    
    void Start()
    {
        tilemap = this.GetComponent<Tilemap>();
    }

    private void FloodFill(Vector3Int tilemapPosition, TileBase fillTile)
    {
        if (tilemap.HasTile(tilemapPosition) && tilemap.GetTile(tilemapPosition) != outlineTile && tilemap.GetTile(tilemapPosition) != fillTile)
        {
            tilemap.SetTile(tilemapPosition, fillTile);
            FloodFill(new Vector3Int(tilemapPosition.x+1, tilemapPosition.y, tilemapPosition.z), fillTile);
            FloodFill(new Vector3Int(tilemapPosition.x-1, tilemapPosition.y, tilemapPosition.z), fillTile);
            FloodFill(new Vector3Int(tilemapPosition.x, tilemapPosition.y+1, tilemapPosition.z), fillTile);
            FloodFill(new Vector3Int(tilemapPosition.x, tilemapPosition.y-1, tilemapPosition.z), fillTile);
        }
    } 
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left Mouse Button Click
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;

            Vector3Int tilemapPosition = tilemap.WorldToCell(mousePosition);
            Debug.Log(tilemapPosition);
            if(tilemapPosition.x >= -17 && tilemapPosition.x <= 17 &&
            tilemapPosition.y >= -17 && tilemapPosition.y <= 17)
            {
                TileBase clickedTile = tilemap.GetTile(tilemapPosition);
                if (clickedTile != outlineTile)
                {
                    //tilemap.SetTile(tilemapPosition, selectedTile);
                    FloodFill(tilemapPosition, selectedTile);
                }
            }
            else if(tilemap.HasTile(tilemapPosition) && tilemap.GetTile(tilemapPosition) != outlineTile)
            {
                selectedTile = tilemap.GetTile(tilemapPosition);
            }
        }
        int count = 0;
        for (int i = 0; i < checkPositions.Count; i++)
        {
            if(tilemap.GetTile(checkPositions[i]) == correctTiles[i])
            {
                count++;
            }
        }
        if(count == checkPositions.Count)
        {
            winText.text = "You Win!!!";
        }
    }
}
