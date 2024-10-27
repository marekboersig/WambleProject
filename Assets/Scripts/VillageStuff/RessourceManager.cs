using System;
using UnityEngine;

public class RessourceManager : MonoBehaviour
{
    private float wood = 0, clay = 0, iron = 0;
    public int woodProd = 1, clayProd = 1, ironProd = 1;
    public int maxCap = 5000;

    private void Update()
    {
        calcRessouces();
    }
    public void add(RessSet ress)
    {
        wood = wood + ress.wood > maxCap ? maxCap : wood + ress.wood;
        clay = clay + ress.clay > maxCap ? maxCap : clay + ress.clay;
        iron = iron + ress.iron > maxCap ? maxCap : iron + ress.iron;
    }

    public void subtract(RessSet ress)
    {
        if (!check(ress))
        {
            Debug.Log("Should not be called");
            return;
        }

        wood -= ress.wood;
        clay -= ress.clay;
        iron -= ress.iron;
    }

    public bool check(RessSet ress)
    {
        if (wood >= ress.wood && clay >= ress.clay && iron >= ress.iron) return true;

        return false;
    }

    public RessSet getCurrent()
    { 
        return new RessSet{wood = (int) Math.Floor(wood), clay = (int) Math.Floor(clay), iron = (int) Math.Floor(iron)};
    }

    public void calcRessouces()
    {
        wood = wood + woodProd * Time.deltaTime > maxCap ? maxCap : wood + woodProd * Time.deltaTime;
        clay = clay + clayProd * Time.deltaTime > maxCap ? maxCap : clay + clayProd * Time.deltaTime;
        iron = iron + ironProd * Time.deltaTime > maxCap ? maxCap : iron + ironProd * Time.deltaTime;
    }
}