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


    /// <summary>
    /// Sets the max value the slider should have.
    /// </summary>
    /// <param name="max">The max value to set.</param>
    protected void SetMaxValue(float max)
    {
        slider.maxValue = max;
    }

    /// <summary>
    /// Sets the visuals of the bar to a new value.
    /// </summary>
    /// <param name="value">The value to set the bar to.</param>
    /// <param name="colors">The color to display the bar in.</param>
    public void SetValue(float value, Colors colors)
    {
        value = (float)System.Math.Round(value, 1);

        slider.value = value;
        text.text = value.ToString();
        var rgbt = colors.GetGradientColor(value);
        slider.transform.GetChild(2).GetComponent<Image>().color = new Color(rgbt[0], rgbt[1], rgbt[2], 1f);

    }


    /// <summary>
    /// Used to initialize the bar. Should be called at the start of the program.
    /// </summary>
    public abstract void Init(Colors color);
}