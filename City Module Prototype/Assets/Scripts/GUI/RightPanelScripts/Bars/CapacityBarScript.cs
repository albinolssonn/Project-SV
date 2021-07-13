
public class CapacityBarScript : Bars
{
    public override void Init(Colors color)
    {
        SetMaxValue((float)GridManager.baseCapacity);
        SetValue(0, color);
    }
}