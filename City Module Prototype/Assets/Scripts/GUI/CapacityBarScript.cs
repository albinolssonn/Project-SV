using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class CapacityBarScript : MonoBehaviour
{
    public Slider slider;
    public TMP_Text text;

    public void Start()
    {
        slider.maxValue = (float)GridManager.baseCapacity;
    }

    /// <summary>
    /// Sets the visuals of the bar to a new value.
    /// </summary>
    /// <param name="signalStr">The value to set the bar to.</param>
    /// <param name="colors">The color to display the bar in.</param>
    public void SetCapacity(float capacity, Colors colors)
    {
        capacity = (float)System.Math.Round(capacity, 1);
        slider.value = capacity;
        text.text = capacity.ToString();
        var rgbt = colors.GetGradientColor(capacity);
        slider.transform.GetChild(2).GetComponent<Image>().color = new Color(rgbt[0], rgbt[1], rgbt[2], 1f);

    }
}
