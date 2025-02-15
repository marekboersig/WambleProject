using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class MapGenerator : MonoBehaviour
{
    public MapSettings mapSettings;
    public TileCollection tiles;

    public Tilemap baseMap;
    public Tilemap highlightMap;

    /// <summary>
    /// Generate the map with given height and width. Uses a perlin noise height distribution to set the specific tiles.
    /// </summary>
    [ContextMenu("Generate Map")]
    void GenerateMap() 
    {
        Random.InitState(mapSettings.seed);
        
        baseMap.ClearAllTiles();
        highlightMap.ClearAllTiles();

        for (int x = 0; x < mapSettings.mapHeight; x++)
        {
            for (int y = 0; y < mapSettings.mapWidth; y++)
            {
                float perlinValue = Mathf.PerlinNoise(x * mapSettings.noiseScale, y * mapSettings.noiseScale);
                Vector3Int tilePosition = new Vector3Int(x, y, 0);
                highlightMap.SetTile(tilePosition, tiles.emptyTile);

                if (perlinValue < 0.1f)
                {
                    baseMap.SetTile(tilePosition, tiles.water);
                }
                else if (perlinValue < 0.85f)
                {
                    if (Mathf.PerlinNoise(2 * x * mapSettings.forestNoiseScale, 2 * y * mapSettings.forestNoiseScale) > mapSettings.forestRatio)
                        baseMap.SetTile(tilePosition, tiles.woods);
                    else 
                        baseMap.SetTile(tilePosition, tiles.grass);
                }
                else if (perlinValue < 0.95f)
                {
                    baseMap.SetTile(tilePosition, tiles.mountain);
                }
                else 
                {
                    baseMap.SetTile(tilePosition, tiles.snow);
                }
            }
        }
    }
}
