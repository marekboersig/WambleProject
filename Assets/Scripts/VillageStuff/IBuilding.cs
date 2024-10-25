public interface IBuilding
{
    public RessSet productionCost();

    public void levelUp(int level = 1);
}

public enum BuildingType
{
    MAIN_BUILDING,
    WOOD_PROD,
    CLAY_PROD,
    IRON_PROD,
    STORAGE,
    FARM
}
