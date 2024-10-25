using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingData", menuName = "Scriptable Objects/BuildingData")]
public class BuildingData : ScriptableObject
{
    [SerializeField] private static BuildingData _instance;
    public static BuildingData Instance 
    { get
        {
            if (_instance == null)
            {
                _instance = Resources.Load(typeof(BuildingData).Name) as BuildingData;
                _instance.initializeBuildingCosts();
            }
            return _instance;
        }
    }

    private const int MAIN_BUILDING_MAX = 2;
    private const int WOOD_PROD_MAX = 2;
    // add further max levels here


    private int[] productionValues = new int[10] {
        10, 20, 30, 40, 50, 60, 70, 80, 90, 100
    };

    private Dictionary<BuildingType, int[,]> buildingCosts;

    public int getProductionValue(int level)
    {
        if (level >= productionValues.Length)
        {
            Debug.Log("Why?");
            return 0;
        }

        return productionValues[level];
    }

    public FullRessSet getCost(BuildingType buildingType, int level) // change to FullRessSet
    {
        int[] tmp = GetRow(buildingCosts[buildingType], level);
        return new FullRessSet { wood = tmp[0], clay = tmp[1], iron = tmp[2], time = tmp[3], pop = tmp[4] };
    }

    private void initializeBuildingCosts()
    {
        buildingCosts = new Dictionary<BuildingType, int[,]>();
        buildingCosts[BuildingType.MAIN_BUILDING] = new int[MAIN_BUILDING_MAX, 5]
        {
            { 10, 10, 10, 4, 5},
            { 10, 10, 10, 4, 5},
        };

        buildingCosts[BuildingType.WOOD_PROD] = new int[WOOD_PROD_MAX, 5]
        {
            { 10, 10, 10, 4, 5 },
            { 10, 10, 10, 4, 5 }
        };

        // add further building costs here
    }

    private int[] GetRow(int[,] matrix, int rowIndex)
    {
        return Enumerable.Range(0, matrix.GetLength(1))
            .Select(x => matrix[rowIndex, x])
            .ToArray();
    }
}
