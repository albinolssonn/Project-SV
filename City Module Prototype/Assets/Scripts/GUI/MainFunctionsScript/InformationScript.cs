using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class InformationScript : MonoBehaviour
{
    public TMP_Text informationText;
    private Queue<string> queue;

    public void Start()
    {
        queue = new Queue<string>();
    }


    /// <summary>
    /// Sets the textfield on the screen to the given string for a short period of time.
    /// </summary>
    /// <param name="input">The string to display on the screen.</param>
    public void SetInformationText(string input)
    {
        if (queue.Count == 0)
        {
            queue.Enqueue(input);
            StartCoroutine(ShowText());
        }
        queue.Enqueue(input);
    }

    /// <summary>
    /// Shows the next message in queue.
    /// </summary>
    private IEnumerator ShowText()
    {
        informationText.text = queue.Peek();
        yield return new WaitForSeconds(3);
        informationText.text = "";

        queue.Dequeue();
        if (queue.Count > 0)
        {
            StartCoroutine(ShowText());
        }
    }
}
