
public class CapacityBarScript : Bars
{
    public void Start()
    {
        SetMaxValue((float)GridManager.baseCapacity);
    }
}