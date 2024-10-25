using UnityEngine;

public class Village : MonoBehaviour
{
    public Coordinates coords;
    public RessourceManager ressourceManager;
    private IBuilding[] buildings;

    private void Start()
    {
        ressourceManager = new RessourceManager();

        buildings = new IBuilding[3] {
            new ProductionBuilding(BuildingType.WOOD_PROD, ressourceManager), 
            new ProductionBuilding(BuildingType.CLAY_PROD, ressourceManager), 
            new ProductionBuilding(BuildingType.IRON_PROD, ressourceManager)
        };
    }

    private void Update()
    {
        ressourceManager.calcRessouces();
    }
}

public class Coordinates
{
    int x, y;
}
