using Unity.VisualScripting;
using UnityEngine;

public class Village : MonoBehaviour
{
    public Coordinates coords;
    public RessourceManager ressourceManager;
    public BuildingManager buildingManager;
    private IBuilding[] buildings;   

    private void Start()
    {
        ressourceManager = this.AddComponent<RessourceManager>();    

        buildings = new IBuilding[4] {
            new ProductionBuilding(BuildingType.WOOD_PROD, ressourceManager),
            new ProductionBuilding(BuildingType.WOOD_PROD, ressourceManager),
            new ProductionBuilding(BuildingType.CLAY_PROD, ressourceManager),
            new ProductionBuilding(BuildingType.IRON_PROD, ressourceManager)
        };
        buildingManager = this.AddComponent<BuildingManager>().setup(buildings);
    }

    private void Update()
    {
        //RessSet tmp = ressourceManager.getCurrent();
        //Debug.Log(tmp.wood + "  " + tmp.clay + "  " + tmp.iron);
    }

    [ContextMenu("TestBuilding")]
    public void BuildIfPossible()
    {
        RessSet tmp = buildings[0].productionCost().getRessSet();
        if (ressourceManager.check(tmp))
        {
            ressourceManager.subtract(tmp);
            buildings[0].levelUp();
            Debug.LogWarning("Upgraded Wood");
        }
        else
        {
            Debug.LogWarning("Not enough!");
        }
    }

    [ContextMenu("build wood")]
    public void woodInQueue()
    {
        buildingManager.fillBuildingQueue(BuildingType.WOOD_PROD);
    }
}

public class Coordinates
{
    int x, y;
}
