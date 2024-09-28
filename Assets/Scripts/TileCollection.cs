using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Tiles", menuName = "Game/Tile Collection", order = 1)]
public class TileCollection : ScriptableObject
{
    public TileBase grass;
    public TileBase water;
    public TileBase mountain;
    public TileBase snow;
    public TileBase woods;

    public TileBase emptyTile;
    public TileBase highlightTile;

    // add further tile types here

    public string GetTileName(TileBase tile)
    {
        if (tile == grass) return "Grass";
        if (tile == water) return "Water";
        if (tile == mountain) return "Mountain";
        if (tile == snow) return "Snow";
        if (tile == woods) return "Woods";
        if (tile == emptyTile) return "Empty";
        if (tile == highlightTile) return "Highlight";

        return "Unknown";
    }
}
