using UnityEngine;
using TMPro;
using UnityEngine.Tilemaps;


public class PopupCoordinates : MonoBehaviour
{
    public TextMeshProUGUI coordinatesTitle;
    public TextMeshProUGUI coordinatesText;
    public RectTransform popupRect; // RectTransform des Popups
    
    private TileCollection tiles;
    private Tilemap baseMap;
    private MapSettings mapSettings;

    void Start()
    {
        gameObject.SetActive(true);
        InitializeReferences();
    }

    private void OnEnable()
    {
        InitializeReferences();
    }

    void Update()
    {
        if (baseMap == null || mapSettings == null)
        {
            InitializeReferences();
            return;
        }

        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3Int cellPosition = baseMap.WorldToCell(worldPosition);

        int tileX = Mathf.FloorToInt(worldPosition.x);
        int tileY = Mathf.FloorToInt(worldPosition.y);

        // Tile an der aktuellen Zelle abfragen
        TileBase tile = baseMap.GetTile(cellPosition);
        string tileTitle = tiles.GetTileName(tile); 

        coordinatesTitle.text = tileTitle;
        coordinatesTitle.text += "  (" + tileX + "/" + tileY + ")";
        coordinatesText.text = "Points: ";
        coordinatesText.text += "\nPlayer: ";
        coordinatesText.text += "\nTrib: ";

        // Set the popup position slightly to the botton right of the mouse position
        float popupPosX = mousePosition.x + popupRect.rect.width / 1.8f;
        float popupPosY = mousePosition.y - popupRect.rect.height / 1.8f;
        Vector3 dummy = Camera.main.WorldToScreenPoint(new Vector3(mapSettings.mapWidth, 0, 0));

        // Set the popup position slightly to the top left of the mouse position when map edge is near
        if ((popupPosX + popupRect.rect.width / 2f) > dummy.x)
            popupPosX = mousePosition.x - popupRect.rect.width / 1.8f;
        if ((popupPosY - popupRect.rect.height / 2f) < dummy.y)
            popupPosY = mousePosition.y + popupRect.rect.height / 1.8f;

        popupRect.position = new Vector3(popupPosX, popupPosY, mousePosition.z); // 50f ist der Offset nach rechts
    }

    void InitializeReferences()
    {
        if (MapGenerator.Instance != null)
        {
            baseMap = MapGenerator.Instance.baseMap;
            mapSettings = MapGenerator.Instance.mapSettings;
            tiles = MapGenerator.Instance.tiles;
        }
        else
        {
            Debug.LogWarning("MapGenerator instance not found.");
        }
    }
}
