using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class InputScript : MonoBehaviour
{

    public GameObject inputField;
    public GameObject inputLimitedAntennas;
    public GameObject visualTextLimitedAntennas;
    private GridManager gridManager;

    public void Start()
    {
        gridManager = GameObject.FindGameObjectsWithTag("Grid")[0].GetComponent<GridManager>();
    }

    public void EnterMaxValue()
    {
        try
        {
            gridManager.SetAndUpdateMaxAntennas(int.Parse(inputField.GetComponentInChildren<Text>().text));
            inputLimitedAntennas.SetActive(false);
            visualTextLimitedAntennas.SetActive(true);
        }
        catch (System.FormatException)
        {
            Debug.Log("Invalid Input");
        }
        
    }
    
}
