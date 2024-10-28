using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingData", menuName = "Scriptable Objects/BuildingData")]
public class BuildingData : ScriptableObject
{
    private static BuildingData _instance;
    public static BuildingData Instance 
    { 
        get
        {
            if (_instance == null)
            {
                _instance = Resources.Load<BuildingData>(typeof(BuildingData).Name);
            }
            return _instance;
        }
    }

    private bool isInitialized = false;

    private void OnEnable()
    {
        buildingCostCurves = buildingDataList.ToDictionary(item => item.buildingType, item => item.dataSet);

        if (!isInitialized)
        {
            initializeBuildingCosts();
            isInitialized = true;
        }
    }

    private int[] productionValues = new int[10] {
        10, 20, 30, 40, 50, 60, 70, 80, 90, 100
    };

    // attribute for serialization of dictionary
    [SerializeField]
    private List<BuildingDataDictionary> buildingDataList = new List<BuildingDataDictionary>();


    // cost curves for editing in editor
    public Dictionary<BuildingType, BuildingDataSet> buildingCostCurves = new Dictionary<BuildingType, BuildingDataSet>();

    // baked int arrays 
    public Dictionary<BuildingType, int[,]> buildingCosts = new Dictionary<BuildingType, int[,]>();



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
        foreach (BuildingType type in System.Enum.GetValues(typeof(BuildingType)))
        {
            buildingCostCurves[type] = new BuildingDataSet();
            applyCurveData(type, buildingCostCurves[type]);
        }

        // add further building costs here
    }

    private int[] GetRow(int[,] matrix, int rowIndex)
    {
        return Enumerable.Range(0, matrix.GetLength(1))
            .Select(x => matrix[rowIndex, x])
            .ToArray();
    }

    public void printDataSet(BuildingType type)
    {
        string s = "\n";
        for (int i = 0; i < buildingCosts[type].GetLength(0); i++)
        {
            s += $"{buildingCosts[type][i, 0]}, {buildingCosts[type][i, 1]}, {buildingCosts[type][i, 2]}, {buildingCosts[type][i, 3]}, {buildingCosts[type][i, 4]} \n";
        }

        Debug.Log(s);
    }

    public void applyCurveData(BuildingType type, BuildingDataSet data)
    {
        int[,] productionCosts = new int[data.maxLevel + 1, 5];

        for (int level = 1; level < productionCosts.GetLength(0); level++)
        {
            productionCosts[level, 0] = Mathf.FloorToInt(data.woodCurve.Evaluate((float)level / data.maxLevel));
            productionCosts[level, 1] = Mathf.FloorToInt(data.clayCurve.Evaluate((float)level / data.maxLevel));
            productionCosts[level, 2] = Mathf.FloorToInt(data.ironCurve.Evaluate((float)level / data.maxLevel));
            productionCosts[level, 3] = Mathf.FloorToInt(data.timeCurve.Evaluate((float)level / data.maxLevel));
            productionCosts[level, 4] = Mathf.FloorToInt(data.popCurve.Evaluate((float)level / data.maxLevel));
        }

        buildingCosts[type] = productionCosts;
    } 
}

[System.Serializable]
public class BuildingDataDictionary
{
    public BuildingType buildingType;
    public BuildingDataSet dataSet;
}


[System.Serializable]
public class BuildingDataSet
{
    public BuildingDataSet()
    {
        maxLevel = 30;

        woodCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
        clayCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
        ironCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
        timeCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
        popCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
    }

    public AnimationCurve woodCurve;
    public AnimationCurve clayCurve;
    public AnimationCurve ironCurve;

    public AnimationCurve timeCurve;
    public AnimationCurve popCurve;
     
    public int maxLevel;
}

[System.Serializable]
public class ProductionDataSet
{
    public ProductionDataSet()
    {
    }

    public AnimationCurve productionCurve;
}