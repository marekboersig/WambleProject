using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class BuildingManager : MonoBehaviour
{
    private Queue<BuildingType> BuildingQueue;
    private IBuilding[] Buildings;
    public RessourceManager ressourceManager;
    private bool isBuilding = false;

    public BuildingManager setup(IBuilding[] buildings)
    {
        this.Buildings = buildings;
        ressourceManager = GetComponent<RessourceManager>();
        BuildingQueue = new Queue<BuildingType> { };    
        return this;
    }

    void Update()
    {
        if(!isBuilding && BuildingQueue.Count > 0)
        {
            build();
        }
    }

    private void build()
    {
        BuildingType buildingType = BuildingQueue.Peek();
        FullRessSet temp;
        IBuilding building = Buildings[(int)buildingType];
        temp = building.productionCost();
        
        if(!ressourceManager.check(temp.getRessSet()))
        {
            Debug.Log("keine Ressis");
            return;
        }
        isBuilding = true;
        BuildingQueue.Dequeue();
        StartCoroutine(countDown(temp.time, building));
    }

    private IEnumerator countDown(int seconde, IBuilding building)
    {
        Debug.Log("start building");
        yield return new WaitForSeconds(seconde);
        building.levelUp();
        isBuilding = false;
        Debug.Log("finish building");
    }

    public void fillBuildingQueue(BuildingType type)
    {
        BuildingQueue.Enqueue(type);    
    }

    public void cancelBuilding(BuildingType type)
    {
       //BuildingQueue.Where(b => b != type);
    }
}
