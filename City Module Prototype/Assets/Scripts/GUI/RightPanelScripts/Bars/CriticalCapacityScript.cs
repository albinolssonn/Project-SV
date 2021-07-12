

public class CriticalCapacityScript : Bars
{
    public void Start()
    {
        SetMaxValue((float)GridManager.baseCapacity);
    }
}