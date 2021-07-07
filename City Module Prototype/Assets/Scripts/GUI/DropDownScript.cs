using UnityEngine;

public class DropDownScript : MonoBehaviour
{
    private GridManager gridManager;

    public void Start()
    {
        gridManager = GameObject.FindGameObjectsWithTag("Grid")[0].GetComponent<GridManager>();
    }


    public void ChooseSimMode(int value)
    {
        switch (value)
        {
            case 0:
                gridManager.SetSimulationMode("coverage");
                break;

            case 1:
                gridManager.SetSimulationMode("capacity");
                break;

            case 2:
                gridManager.SetSimulationMode("none");
                break;


            default:
                throw new System.Exception("This should be unreachable.");
        }
    }


}
