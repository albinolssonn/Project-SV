
// HACK: Add more color gradients.
// If you want any other color gradients in the program, add them here and derive from Colors.

/// <summary>
/// Contains the color codes for the colors used in the program.
/// </summary>
public abstract class Colors
{

    public static readonly float[] gray = new float[] { 0.5f, 0.5f, 0.5f, 0.50f };
    public static readonly float[] white = new float[] { 1f, 1f, 1f, 0.50f };

    protected readonly double maxValue;

    /// <summary>
    /// Contains the color codes for the colors used in the program.
    /// </summary>
    /// <param name="maxValue">The maximum value of the gradient. Taken into account when applying gradient colors.</param>
    public Colors(double maxValue)
    {
        this.maxValue = maxValue;
    }


    /// <summary>
    /// Calculates the RGB value [0,1] based on the input in relation to 'maxValue'.
    /// </summary>
    /// <param name="value">The value to create a corresponding color for.</param>
    /// <returns>A color from a gradient scale based on 'value'.</returns>
    public abstract float[] GetGradientColor(double value);
}


public class RedGreen : Colors
{
    public RedGreen(double maxValue) : base(maxValue) { }

    public override float[] GetGradientColor(double value)
    {
        float k = (float)(2 / (maxValue - 1));
        float rValue = (float)System.Math.Min(1, -k * value + k + 2);
        float gValue = (float)System.Math.Min(1, k * value - k);

        return new float[4] { rValue, gValue, 0f, 0.50f };
    }
}

public class WhiteBlue : Colors
{
    public WhiteBlue(double maxValue) : base(maxValue) { }


    public override float[] GetGradientColor(double value)
    {
        if(value < 0)
        {
            return new float[4] { 1f, 0f, 0f, 0.50f };
        }
        float k = (float)(1 / maxValue);
        float rValue = (float)System.Math.Min(1, -k * value + 1);
        float gValue = (float)System.Math.Min(1, -k * value + 1);

        return new float[4] { rValue, gValue, 1.00f, 0.50f };
    }
}