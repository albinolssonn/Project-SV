using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacer : MonoBehaviour
{
    GridManager grid;

    public void Start()
    {
        grid = GameObject.FindGameObjectsWithTag("Grid")[0].GetComponent<GridManager>();

    }

    public void Antenna_Btn()
    {
        grid.SetToBePlaced(new Antenna()); 
    }

    public void RemoveAntenna_Btn()
    {
        grid.SetToBeRemoved(new Antenna());
    }

    public void House_Btn()
    {
        grid.SetToBePlaced(new House());
    }

    public void RemoveHouse_Btn()
    {
        grid.SetToBeRemoved(new House());
    }


}
