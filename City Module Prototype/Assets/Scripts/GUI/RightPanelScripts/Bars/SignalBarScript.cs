
public class SignalBarScript : Bars
{

    public override void Init(Colors color)
    {
        SetMaxValue((float)GridManager.baseSignalStr);
        SetValue(0, color);
    }
}