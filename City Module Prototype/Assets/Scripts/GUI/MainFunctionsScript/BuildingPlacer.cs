using UnityEngine;

// HACK: Place module buttons.
// If you want to add any button to place any additional modules you create, create the button method here.
// Create a method where you call 'grid.SetToBePlaced(new MODULE())' with MODULE being the module class you've created.
// Then attach LeftSide_Panel to your button in Unity and call the method you created here on click and it's done.

/// <summary>
/// Contains the functionality of every button used to place and remove modules on the grid.
/// </summary>
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
        grid.SelectToBePlaced(new Antenna());
    }

    public void RemoveAntenna_Btn()
    {
        grid.SetToBeRemoved(new Antenna());
    }

    //------------------------------------------------------

    public void House_Btn()
    {
        grid.SelectToBePlaced(new House());
    }

    public void RemoveHouse_Btn()
    {
        grid.SetToBeRemoved(new House());
    }

    //------------------------------------------------------

    public void TallBuilding_Btn()
    {
        grid.SelectToBePlaced(new TallBuilding());
    }

    public void RemoveTallBuilding_Btn()
    {
        grid.SetToBeRemoved(new TallBuilding());
    }

    //------------------------------------------------------

    public void Park_Btn()
    {
        grid.SelectToBePlaced(new Park());
    }

    public void RemovePark_Btn()
    {
        grid.SetToBeRemoved(new Park());
    }

    //------------------------------------------------------

    public void Hospital_Btn()
    {
        grid.SelectToBePlaced(new Hospital());
    }

    public void RemoveHospital_Btn()
    {
        grid.SetToBeRemoved(new Hospital());
    }

    //------------------------------------------------------

    public void PoliceStation_Btn()
    {
        grid.SelectToBePlaced(new PoliceStation());
    }

    public void RemovePoliceStation_Btn()
    {
        grid.SetToBeRemoved(new PoliceStation());
    }

    //------------------------------------------------------

    public void FireDepartment_Btn()
    {
        grid.SelectToBePlaced(new FireDepartment());
    }

    public void RemoveFireDepartment_Btn()
    {
        grid.SetToBeRemoved(new FireDepartment());
    }

    //------------------------------------------------------


}