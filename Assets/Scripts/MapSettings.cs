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
