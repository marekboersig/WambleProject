using UnityEngine;
using UnityEngine.Tilemaps;

public class TileHighlighting : MonoBehaviour
{
    public Tilemap baseMap;
    public Tilemap highlightMap;

    public TileCollection tiles;

    private Vector3Int previousPosition;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPosition = baseMap.WorldToCell(mouseWorldPos);

            if (highlightMap.HasTile(cellPosition))
            {
                if (previousPosition != cellPosition)
                {
                    highlightMap.SetTile(previousPosition, tiles.emptyTile);
                }

                previousPosition = cellPosition;
                highlightMap.SetTile(cellPosition, tiles.highlightTile);
            }
        }
    }
}
