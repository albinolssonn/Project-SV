using UnityEngine;
using UnityEngine.UI;

public class InputScript : MonoBehaviour
{

    public InputField inputLimitedAntennas;
    public GameObject visualTextLimitedAntennas;
    private GridManager gridManager;
    private GameObject toggle;

    public void Start()
    {
        gridManager = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridManager>();
        toggle = GameObject.Find("LimitedAntennas_Toggle");

    }


    /// <summary>
    /// Sends the input of maximum number of Antennas for the "Limited Antennas" mode from the input field to the grid manager.
    /// </summary>
    public void EnterMaxValue()
    {
        string input = inputLimitedAntennas.GetComponentInChildren<Text>().text;
        if (!input.Equals(""))
        {
            try
            {
                gridManager.SetAndUpdateMaxAntennas(int.Parse(inputLimitedAntennas.GetComponentInChildren<Text>().text));
                inputLimitedAntennas.gameObject.SetActive(false);
                visualTextLimitedAntennas.SetActive(true);
            }
            catch (System.FormatException)
            {
                Debug.Log("Invalid Input");

            }
        }
        else
        {
            toggle.GetComponent<Toggle>().isOn = false;
        }
    }
}
