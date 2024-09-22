using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    public MapSettings mapSettings;
    public TileCollection tiles;

    public Tilemap tilemap;

    void Start()
    {
        // initiate with the seed given in the settings and generate map
        Random.InitState(mapSettings.seed);
        GenerateMap();   
    }

    /// <summary>
    /// Generate the map with given height and width. Uses a perlin noise height distribution to set the specific tiles.
    /// </summary>
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
                    tilemap.SetTile(tilePosition, tiles.water);
                }
                else if (perlinValue < 0.85f)
                {
                    if (Mathf.PerlinNoise(2 * x * mapSettings.forestNoiseScale, 2 * y * mapSettings.forestNoiseScale) > mapSettings.forestRatio)
                        tilemap.SetTile(tilePosition, tiles.woods);
                    else 
                        tilemap.SetTile(tilePosition, tiles.grass);
                }
                else if (perlinValue < 0.95f)
                {
                    tilemap.SetTile(tilePosition, tiles.mountain);
                }
                else 
                {
                    tilemap.SetTile(tilePosition, tiles.snow);
                }
            }
        }
    }
}
