using UnityEngine;
using TMPro;


public class GridSizeScript : MonoBehaviour
{
    public TMP_InputField rows_input;
    public TMP_InputField cols_input;
    public GameObject gridSizeUi;
    public GameObject settingsPageUi;

    private GridManager gridManager;

    public void Start()
    {
        gridManager = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridManager>();
    }


    /// <summary>
    /// Sets the grid to a new size with values taken from the fields 'rows_input' and 'cols_input'.
    /// </summary>
    public void SetGridSize()
    {
        int rows, cols;

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
            return;
        }

        rows_input.text = "";
        cols_input.text = "";
        gridManager.SetNewGridSize(rows, cols);
    }
}
