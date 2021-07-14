using UnityEngine;

// HACK: Here you can add button methods to create additional pre-configured cities.
// First create a method in the class 'PreConfCities' as instructed in the method 'LoadPreconfigCity'
// and then call on 'LoadPreconfigCity' with the correct index as you define in the switch case.

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
