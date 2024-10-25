public class RessSet
{
    public int wood;
    public int clay;
    public int iron;    
}

public class FullRessSet
{
    public int wood;
    public int clay;
    public int iron;
    public int time;
    public int pop;

    public RessSet getRessSet()
    {
        return new RessSet { wood = wood, clay = clay, iron = iron };
    }
}
