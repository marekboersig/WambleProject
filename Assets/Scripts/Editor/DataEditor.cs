using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BuildingData))]
public class DataEditor : Editor
{
    private BuildingData buildingData;

    bool[] dataBools;

    private void OnEnable()
    {
        buildingData = (BuildingData)target;

        if (buildingData == null)
        {
            Debug.Log("BuildingData is null");
            return;
        }

        dataBools = new bool[buildingData.buildingCostCurves.Count];
    }

    public override void OnInspectorGUI()
    {
        int i = 0;

        foreach (BuildingType type in buildingData.buildingCostCurves.Keys) 
        {
            dataBools[i] = EditorGUILayout.Foldout(dataBools[i], $"Show Settings: {type.ToString()}");

            if (dataBools[i]) 
            {
                DrawBuildingCostSettings(buildingData.buildingCostCurves[type]);
            }
            i++;
        }
    }

    private void DrawBuildingCostSettings(BuildingDataSet data)
    {
        data.maxLevel = EditorGUILayout.IntField("MaxLevel", data.maxLevel);

        EditorGUILayout.Space();

        data.woodCurve = EditorGUILayout.CurveField("Wood Cost", data.woodCurve);
        data.clayCurve = EditorGUILayout.CurveField("Clay Cost", data.clayCurve);
        data.ironCurve = EditorGUILayout.CurveField("Iron Cost", data.ironCurve);
        data.timeCurve = EditorGUILayout.CurveField("Time to build", data.timeCurve);
        data.popCurve = EditorGUILayout.CurveField("Pop Cost", data.popCurve);

        EditorGUILayout.Space();

        if (GUILayout.Button("Apply"))
        {
            buildingData.applyCurveData(BuildingType.WOOD_PROD, data);
        }
    }
}


