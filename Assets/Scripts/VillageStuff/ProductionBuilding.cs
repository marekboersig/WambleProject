using UnityEngine;

public class ProductionBuilding : IBuilding
{
    private BuildingType _buildingType;
    private int currentLevel = 0;
    private int maxLevel = 30;

    private RessourceManager _ressourceManager;

    public ProductionBuilding(BuildingType buildingType, RessourceManager ressourceManager)
    {
        if (buildingType != BuildingType.WOOD_PROD && buildingType != BuildingType.CLAY_PROD && buildingType != BuildingType.IRON_PROD)
        {
            Debug.Log("Not valid production building");
            return;
        }

        _ressourceManager = ressourceManager;
        _buildingType = buildingType;
    }

    void IBuilding.levelUp(int level)
    {
        if (currentLevel + level > maxLevel)
        {
            Debug.Log("MaxLevel überschritten");
            return;
        }
        currentLevel += level;

        int updatedProduction = BuildingData.Instance.getProductionValue(currentLevel);

        switch (_buildingType) { 
            case BuildingType.WOOD_PROD:
                _ressourceManager.woodProd = updatedProduction;
                break;
            case BuildingType.CLAY_PROD:
                _ressourceManager.clayProd = updatedProduction;
                break;
            case BuildingType.IRON_PROD:
                _ressourceManager.ironProd = updatedProduction;
                break;
            default:
                break;
        }  
    }

    RessSet IBuilding.productionCost()
    {
        // fetch prod cost
        // return it
        return null;
    }
}
