using UnityEngine;
using TMPro;


public class GridSizeScript : MonoBehaviour
{
    public TMP_InputField rows_input;
    public TMP_InputField cols_input;
    public GameObject gridSizeUi;
    public GameObject settingsPageUi; 

    private GridManager gridManager;

    private void Start()
    {
        gridManager = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridManager>();
    }

    public void SetGridSize()
    {
        int rows = 0, cols = 0;

        try
        {
            rows = int.Parse(rows_input.text);
            cols = int.Parse(cols_input.text);
        }
        catch (System.FormatException)
        {
            rows_input.text = "";
            cols_input.text = "";
            gridManager.SetErrorMessage("Invalid grid size.");
        }

        rows_input.text = "";
        cols_input.text = "";
        gridManager.SetNewGridSize(rows, cols);

        settingsPageUi.SetActive(true);
        gridSizeUi.SetActive(false); 
        

    }
}
