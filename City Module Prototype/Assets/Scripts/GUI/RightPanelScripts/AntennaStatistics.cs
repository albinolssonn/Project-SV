using TMPro;
using UnityEngine;

public class AntennaStatistics : MonoBehaviour
{

    public TMP_Text text;


    /// <summary>
    /// Sets the number displayed in the UI text field representing the number of Antennas placed.
    /// </summary>
    /// <param name="value">Current number of Antennas</param>
    /// <param name="maxValue">Maximum number of Antennas. Only used if mode "Limited Antennas" is active.</param>
    public void SetAntennaStatistics(int value, int maxValue)
    {
        if (GridManager.limitedAntennasMode)
        {
            text.text = value.ToString() + " / " + maxValue.ToString();
            if (value > maxValue)
            {
                text.color = Color.red;
            }
            else
            {
                text.color = Color.white;
            }
        }
        else
        {
            text.text = value.ToString();
            text.color = Color.white;
        }
    }
}
