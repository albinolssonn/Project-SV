using UnityEngine;

/*
 * This class contains the functionality of every button used to place and remove modules on the grid.
 */
public class BuildingPlacer : MonoBehaviour
{
    GridManager grid;

    public void Start()
    {
        grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridManager>();

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

    public void Hospital_Btn()
    {
        grid.SetToBePlaced(new Hospital());
    }

    public void RemoveHospital_Btn()
    {
        grid.SetToBeRemoved(new Hospital());
    }

    //------------------------------------------------------

    public void PoliceStation_Btn()
    {
        grid.SetToBePlaced(new PoliceStation());
    }

    public void RemovePoliceStation_Btn()
    {
        grid.SetToBeRemoved(new PoliceStation());
    }

    //------------------------------------------------------

    public void FireDepartment_Btn()
    {
        grid.SetToBePlaced(new FireDepartment());
    }

    public void RemoveFireDepartment_Btn()
    {
        grid.SetToBeRemoved(new FireDepartment());
    }

    //------------------------------------------------------


}