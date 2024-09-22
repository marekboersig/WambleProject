using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "MapSettings", menuName = "Game/Map Settings", order = 1)]
public class MapSettings : ScriptableObject
{
    public int mapWidth = 200;
    public int mapHeight = 200;

    public int seed = 0;
    public float noiseScale = 0.07f;

    public float forestNoiseScale = 1.0f;
    public float forestRatio = 0.8f;

    // add further map settings here
}

[CreateAssetMenu(fileName = "Tiles", menuName = "Game/Tile Collection", order = 1)]
public class TileCollection : ScriptableObject
{
    public TileBase grass;
    public TileBase water;
    public TileBase mountain;
    public TileBase snow;
    public TileBase woods;

    // add further tile types here
}
