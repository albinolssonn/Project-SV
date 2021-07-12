using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreBuiltScript : MonoBehaviour
{
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;

    GridManager grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridManager>();

    public void PreCofigBtn1()
    {
        grid.LoadPreconfigCity(0); 
    }

    public void PreCofigBtn2()
    {
        grid.LoadPreconfigCity(1);
    }

    public void PreCofigBtn3()
    {
        grid.LoadPreconfigCity(2);
    }

    public void PreCofigBtn4()
    {
        grid.LoadPreconfigCity(3);
    }
}
