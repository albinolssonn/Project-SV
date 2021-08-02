using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePreBuiltScript : MonoBehaviour
{
    private GridManager grid;

    public void Start()
    {
        grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridManager>();
    }

    public void SaveCofigBtn1()
    {
        grid.SavePreconfigCity("Config1");

    }

    public void SaveCofigBtn2()
    {
        grid.SavePreconfigCity("Config2");

    }

    public void SaveCofigBtn3()
    {
        grid.SavePreconfigCity("Config3");

    }

    public void SaveCofigBtn4()
    {
        grid.SavePreconfigCity("Config4");

    }
}
