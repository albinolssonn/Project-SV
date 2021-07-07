using UnityEngine;
using TMPro;


public class InformationScript : MonoBehaviour
{
    public TMP_Text informationText;

    public void SetInformationText(string input)
    {
        informationText.text = input;
        Invoke("DisableText", 5f); 
    }

    private void DisableText()
    {
        informationText.text = ""; 
    }
}
