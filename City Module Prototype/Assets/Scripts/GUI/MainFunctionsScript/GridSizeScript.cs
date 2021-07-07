using UnityEngine;
using TMPro;
using UnityEngine.UI; 


public class GridSizeScript : MonoBehaviour
{

    public TMP_InputField rows_input;
    public TMP_InputField cols_input;

    public string rows;
    public string cols;

    public void setGridSize()
    {
        rows = rows_input.text;
        cols = cols_input.text; 
        Debug.Log("Rows: " + rows + ", Columns: " + cols); 
    }
}
