using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreBuiltScript : MonoBehaviour
{

    private GridManager grid;
    public GameObject prebuiltCityUi;
    public GameObject pauseMenuUi;


    public void Start()
    {
        grid =  GameObject.FindGameObjectWithTag("Grid").GetComponent<GridManager>();
    }

    public void PreCofigBtn1()
    {
        grid.LoadPreconfigCity(1);

    }

    public void PreCofigBtn2()
    {
        grid.LoadPreconfigCity(2);

    }

    public void PreCofigBtn3()
    {
        grid.LoadPreconfigCity(3);

    }

    public void PreCofigBtn4()
    {
        grid.LoadPreconfigCity(4);

    }
}
