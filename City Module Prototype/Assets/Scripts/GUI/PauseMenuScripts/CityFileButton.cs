using UnityEngine;

public class CityFileButton : MonoBehaviour
{

    private GridManager grid;
    public string fileName;


    public void Start()
    {
        grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridManager>();
    }


    public void ButtonClicked()
    {
        grid.LoadPreconfigCity(fileName);
    }
}
