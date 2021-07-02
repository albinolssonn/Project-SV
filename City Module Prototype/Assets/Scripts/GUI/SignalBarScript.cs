using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Contains the functionality of coverage statistic bar visualized.
/// </summary>
public class SignalBarScript : MonoBehaviour
{

    public Slider slider;
    public TMP_Text text;

    /// <summary>
    /// Sets the visuals of the bar to a new value.
    /// </summary>
    /// <param name="signalStr">The value to set the bar to.</param>
    /// <param name="colors">The color to display the bar in.</param>
    public void SetCoverage(float signalStr, Colors colors)
    {
        slider.value = signalStr;
        text.text = signalStr.ToString();
        var rgbt = colors.GetGradientColor(signalStr);
        slider.transform.GetChild(2).GetComponent<Image>().color = new Color(rgbt[0], rgbt[1], rgbt[2], 1f);

    }
    
}