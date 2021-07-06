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
                Debug.Log(value + " = Signal Strength");
                gridManager.SetSimulationMode("coverage");
                break;

            case 1:
                Debug.Log(value + " = Capacity");
                gridManager.SetSimulationMode("capacity");
                break;

            default:
                throw new System.Exception("This should be unreachable.");
        }
    }


}
