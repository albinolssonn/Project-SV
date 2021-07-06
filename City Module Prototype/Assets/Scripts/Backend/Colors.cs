
using UnityEngine;
/// <summary>
/// Contains the color codes for the colors used in the program.
/// </summary>
public abstract class Colors
{

    public static readonly float[] gray = new float[] { 0.5f, 0.5f, 0.5f, 0.50f };
    public static readonly float[] lightOrange = new float[] { 1f, 1f, 0.425f, 0.50f };
    public static readonly float[] red = new float[] { 1f, 0.276f, 0.231f, 0.50f };
    public static readonly float[] green = new float[] { 0.23f, 1f, 0.325f, 0.50f };
    
    protected readonly double maxValue;

    /// <summary>
    /// Contains the color codes for the colors used in the program.
    /// </summary>
    /// <param name="maxValue">The maximum signal strength which a Cell can have. Taken into account when applying gradient colors.</param>
    public Colors(double maxValue)
    {
        this.maxValue = maxValue; 
    }


    /// <summary>
    /// Calculates the RGB value [0,1] based on the maximum signal strength and the given signal strength.
    /// </summary>
    /// <param name="signalStr">The value to create a corresponding color for going from red to green.</param>
    /// <returns>A color between red (low value) and green (high value).</returns>
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
        float k = (float)(1 / maxValue);
        float rValue = (float)System.Math.Min(1, k * value);
        float gValue = (float)System.Math.Min(1, k * value);

        return new float[4] { rValue, gValue, 1.00f, 0.50f };
    }
}