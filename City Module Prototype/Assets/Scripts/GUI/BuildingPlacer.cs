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






}
