using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.Rendering.RayTracingAccelerationStructure;

public class TileHighlighting : MonoBehaviour
{
    private Tilemap baseMap;
    private Tilemap highlightMap;
    private TileCollection tiles;

    private Vector3Int previousPosition;

    private void Start()
    {
        InitializeReferences();
    }

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

    void InitializeReferences()
    {
        if (MapGenerator.Instance != null)
        {
            baseMap = MapGenerator.Instance.baseMap;
            highlightMap = MapGenerator.Instance.highlightMap;
            tiles = MapGenerator.Instance.tiles;
        }
        else
        {
            Debug.LogWarning("MapGenerator instance not found.");
        }
    }
}
