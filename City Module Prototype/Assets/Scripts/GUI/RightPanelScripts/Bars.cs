using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Contains the functionality of statistic bars visualized.
/// </summary>
public abstract class Bars : MonoBehaviour 
{
    public Slider slider;
    public TMP_Text text;


    protected void SetMaxValue(float max)
    {
        slider.maxValue = max;
    }

    /// <summary>
    /// Sets the visuals of the bar to a new value.
    /// </summary>
    /// <param name="signalStr">The value to set the bar to.</param>
    /// <param name="colors">The color to display the bar in.</param>
    public void SetValue(float signalStr, Colors colors)
    {
        signalStr = (float)System.Math.Round(signalStr, 1);

        slider.value = signalStr;
        text.text = signalStr.ToString();
        var rgbt = colors.GetGradientColor(signalStr);
        slider.transform.GetChild(2).GetComponent<Image>().color = new Color(rgbt[0], rgbt[1], rgbt[2], 1f);

    }
}