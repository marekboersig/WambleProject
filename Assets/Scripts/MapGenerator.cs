using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    public MapSettings mapSettings;

    public Tilemap tilemap;
    public TileBase woodTile;
    public TileBase grassTile;
    public TileBase waterTile;
    public TileBase sandTile;

    void Start()
    {
        GenerateMap();   
    }

    void GenerateMap() 
    {
        for (int x = 0; x < mapSettings.mapHeight; x++)
        {
            for (int y = 0; y < mapSettings.mapWidth; y++)
            {
                float perlinValue = Mathf.PerlinNoise(x * mapSettings.noiseScale, y * mapSettings.noiseScale);
                Vector3Int tilePosition = new Vector3Int(x, y, 0);

                if (perlinValue < 0.1f)
                {
                    tilemap.SetTile(tilePosition, waterTile);
                }
                else if (perlinValue < 0.8f)
                {
                    tilemap.SetTile(tilePosition, grassTile);
                }
                else if (perlinValue < 0.95f)
                {
                    tilemap.SetTile(tilePosition, woodTile);
                }
                else 
                {
                    tilemap.SetTile(tilePosition, sandTile);
                }
            }
        }
    }
}
