using TMPro;
using UnityEngine;
using UnityEngine.UI; 

public class SignalBarScript : MonoBehaviour
{

    public Slider slider;
    public TMP_Text text;

    public void SetCoverage(float signalStr)
    {
        slider.value = signalStr;
        text.text = signalStr.ToString();
    }
    
}
