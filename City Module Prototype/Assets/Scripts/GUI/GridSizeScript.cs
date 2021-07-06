using UnityEngine;
using TMPro;
using UnityEngine.UI; 


public class GridSizeScript : MonoBehaviour
{
    public TMP_InputField rows;
    public TMP_InputField cols;
 
    public void EnterGridInput()
    {

        Debug.Log("Rows: " + rows.text);
        Debug.Log("Cols: " + cols.text);

    }
}
