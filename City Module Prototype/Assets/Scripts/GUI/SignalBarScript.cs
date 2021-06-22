using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class SignalBarScript : MonoBehaviour
{

    public Slider slider; 

    public void SetCoverage(float signalStr)
    {
        slider.value = signalStr; 
    }
    
}
