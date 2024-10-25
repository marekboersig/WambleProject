using UnityEngine;

public class BuildingData : MonoBehaviour 
{
    public static BuildingData Instance { get; private set; }
    
    private int[] productionValues = new int[10] { 
        10, 20, 30, 40, 50, 60, 70, 80, 90, 100
    };

    public int getProductionValue(int level)
    {
        if (level >= productionValues.Length)
        {
            Debug.Log("Why?");
            return 0;
        }

        return productionValues[level];
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else 
        {
            Instance = this;
        }
    }
}
