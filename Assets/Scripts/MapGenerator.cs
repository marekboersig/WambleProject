using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase woodTile;
    public TileBase grassTile;
    public TileBase waterTile;
    public TileBase sandTile;

    public int mapWidth = 200;
    public int mapHeight = 200;

    public float noiseScale = 0.1f;

    void Start()
    {
        GenerateMap();   
    }

    void GenerateMap() 
    {
        for (int x = 0; x < mapHeight; x++)
        {
            for (int y = 0; y < mapWidth; y++)
            {
                float perlinValue = Mathf.PerlinNoise(x * noiseScale, y * noiseScale);
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
