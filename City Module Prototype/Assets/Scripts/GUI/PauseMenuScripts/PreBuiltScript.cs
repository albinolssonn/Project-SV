using UnityEngine;

public class PreBuiltScript : MonoBehaviour
{

    private GridManager grid;
    public GameObject prebuiltCityUi;
    public GameObject pauseMenuUi;


    public void Start()
    {
        grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridManager>();
    }


    /// <summary>
    /// Loads a pre-configured city.
    /// </summary>
    public void PreCofigBtn1()
    {
        grid.LoadPreconfigCity(1);

    }


    /// <summary>
    /// Loads a pre-configured city.
    /// </summary>
    public void PreCofigBtn2()
    {
        grid.LoadPreconfigCity(2);

    }


    /// <summary>
    /// Loads a pre-configured city.
    /// </summary>
    public void PreCofigBtn3()
    {
        grid.LoadPreconfigCity(3);

    }


    /// <summary>
    /// Loads a pre-configured city.
    /// </summary>
    public void PreCofigBtn4()
    {
        grid.LoadPreconfigCity(4);

    }
}
