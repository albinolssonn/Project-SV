using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class contains the color codes for the colors used in the program.
 */
public class Colors
{

    public readonly float[] gray = new float[] { 0.5f, 0.5f, 0.5f, 0.50f };
    public readonly float[] lightOrange = new float[] { 1f, 1f, 0.425f, 0.50f };
    public readonly float[] red = new float[] { 1f, 0.276f, 0.231f, 0.50f };
    public readonly float[] green = new float[] { 0.23f, 1f, 0.325f, 0.50f };
    private readonly double maxSignalStr; 

    public  Colors(double maxSignalStr)
    {
        this.maxSignalStr = maxSignalStr; 
    }

    public float[] GetGradientColor(double signalStr)
    {
        float k = (float) (2/((maxSignalStr - 1)));
        float rValue = (float) System.Math.Min(1, -k * signalStr + k + 2);
        float gValue = (float) System.Math.Min(1,  k * signalStr - k);

        return new float[4] { rValue, gValue, 0f, 0.50f }; 
    }
}

// -((maxSignalStr - 1) / 2) + 2 + ((maxSignalStr - 1) /2)
