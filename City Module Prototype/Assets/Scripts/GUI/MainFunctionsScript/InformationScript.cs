using UnityEngine;
using TMPro;


public class InformationScript : MonoBehaviour
{
    public TMP_Text informationText;


    /// <summary>
    /// Sets the textfield on the screen to the given string for a short period of time.
    /// </summary>
    /// <param name="input">The string to display on the screen.</param>
    public void SetInformationText(string input)
    {
        informationText.text = input;
        Invoke("DisableText", 5f);
    }


    /// <summary>
    /// Resets the textfield.
    /// </summary>
    /*
     * It is said to be unused, but is called using Invoke() from the method above!
     */
    private void DisableText()
    {
        informationText.text = "";
    }
}
