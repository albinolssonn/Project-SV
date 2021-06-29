using TMPro;
using UnityEngine;
using UnityEngine.UI; 

/*
 * This class contains the functionality of any statistic bars visualized.
 */
public class SignalBarScript : MonoBehaviour
{

    public Slider slider;
    public TMP_Text text;


    public void SetCoverage(float signalStr, Colors colors)
    {
        slider.value = signalStr;
        text.text = signalStr.ToString();
        var rgbt = colors.GetGradientColor(signalStr);
        slider.transform.GetChild(2).GetComponent<Image>().color = new Color(rgbt[0], rgbt[1], rgbt[2], 1f);

    }
    
}
