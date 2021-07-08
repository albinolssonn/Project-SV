using TMPro;
using UnityEngine;

public class AntennaStatistics : MonoBehaviour
{

    public TMP_Text text;


    public void setAntennaStatistics(int value, int maxValue)
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
        }
    }
}
