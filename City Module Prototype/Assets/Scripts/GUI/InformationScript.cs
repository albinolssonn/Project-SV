using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class InformationScript : MonoBehaviour
{
    public TMP_Text informationText;

    public void setText(string input)
    {
        informationText.text = input; 
    }
}
