using UnityEngine;

// HACK: Create pre-configured city button.
// Here you can add button methods to create additional pre-configured cities.
// First create a method in the class 'PreConfCities' as instructed in the method 'LoadPreconfigCity'
// and then call on 'LoadPreconfigCity' with the correct index as you define in the switch case.
// Then attach PrebuiltCities_Panel to your button in Unity and call the method you created here on click and it's done.

public class LoadPreBuiltScript : MonoBehaviour
{

    private GridManager grid;


    public void Start()
    {
        grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridManager>();
    }


    /// <summary>
    /// Loads a pre-configured city.
    /// </summary>
    public void PreCofigBtn1()
    {
        grid.LoadPreconfigCity("Config1");

    }


    /// <summary>
    /// Loads a pre-configured city.
    /// </summary>
    public void PreCofigBtn2()
    {
        grid.LoadPreconfigCity("Config2");

    }


    /// <summary>
    /// Loads a pre-configured city.
    /// </summary>
    public void PreCofigBtn3()
    {
        grid.LoadPreconfigCity("Config3");

    }


    /// <summary>
    /// Loads a pre-configured city.
    /// </summary>
    public void PreCofigBtn4()
    {
        grid.LoadPreconfigCity("Config4");

    }
}
