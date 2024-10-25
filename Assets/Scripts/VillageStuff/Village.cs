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
        RessSet tmp = ressourceManager.getCurrent();
        Debug.Log(tmp.wood + "  " + tmp.clay + "  " + tmp.iron);
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
}

public class Coordinates
{
    int x, y;
}
