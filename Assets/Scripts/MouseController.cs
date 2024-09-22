using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MouseController : MonoBehaviour
{
    public Tilemap tilemap;
    public TileCollection tiles;

    private Vector3Int previousPosition;
    private TileBase previousTileType;

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) 
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPosition = tilemap.WorldToCell(mouseWorldPos);
            TileBase selected = null;
           
            if (previousPosition != null) 
                tilemap.SetTile(previousPosition, previousTileType);

            previousPosition = cellPosition;
            previousTileType = tilemap.GetTile(cellPosition);

            if (previousTileType == tiles.grass)
            {
                selected = tiles.grassSelected;
            }
            else if (previousTileType == tiles.mountain)
            {
                selected = tiles.mountainSelected;
            }
            else if (previousTileType == tiles.woods)
            {
                selected = tiles.woodsSelected;
            }

            if (selected != null)
            {
                tilemap.SetTile(cellPosition, selected);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            tilemap.SetTile(previousPosition, previousTileType);
        }
    }
}
