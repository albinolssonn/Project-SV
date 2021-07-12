using UnityEngine;

public class DropDownScript : MonoBehaviour
{
    private GridManager gridManager;
    public GameObject capacityBar;
    public GameObject coverageBar;
    public GameObject criticalCapacityBar;
    public GameObject criticalCoverageBar;



    public void Start()
    {
        gridManager = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridManager>();
    }


    public void ChooseSimMode(int value)
    {
        switch (value)
        {
            case 0:
                gridManager.SetSimulationMode("coverage");
                capacityBar.SetActive(false);
                coverageBar.SetActive(true);

                if (gridManager.GetCriticalMode())
                {
                    criticalCapacityBar.SetActive(false);
                    criticalCoverageBar.SetActive(true);
                }

                break;

            case 1:
                gridManager.SetSimulationMode("capacity");
                capacityBar.SetActive(true);
                coverageBar.SetActive(false);

                if (gridManager.GetCriticalMode())
                {
                    criticalCapacityBar.SetActive(true);
                    criticalCoverageBar.SetActive(false);
                }

                break;

            case 2:
                gridManager.SetSimulationMode("none");
                capacityBar.SetActive(false);
                coverageBar.SetActive(false);
                criticalCapacityBar.SetActive(false);
                criticalCoverageBar.SetActive(false);
                break;


            default:
                throw new System.Exception("This should be unreachable.");
        }
    }




}
