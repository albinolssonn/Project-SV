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

    //------------------------------------------------------

    public void Antenna_Btn()
    {
        grid.SetToBePlaced(new Antenna()); 
    }

    public void RemoveAntenna_Btn()
    {
        grid.SetToBeRemoved(new Antenna());
    }

    //------------------------------------------------------

    public void House_Btn()
    {
        grid.SetToBePlaced(new House());
    }

    public void RemoveHouse_Btn()
    {
        grid.SetToBeRemoved(new House());
    }

    //------------------------------------------------------

    public void TallBuilding_Btn()
    {
        grid.SetToBePlaced(new TallBuilding());
    }

    public void RemoveTallBuilding_Btn()
    {
        grid.SetToBeRemoved(new TallBuilding());
    }

    //------------------------------------------------------

    public void Park_Btn()
    {
        grid.SetToBePlaced(new Park());
    }

    public void RemovePark_Btn()
    {
        grid.SetToBeRemoved(new Park());
    }

    //------------------------------------------------------



}
