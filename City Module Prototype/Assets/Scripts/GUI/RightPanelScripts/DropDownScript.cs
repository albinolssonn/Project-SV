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
                capacityBar.SetActive(false);
                coverageBar.SetActive(true);

                if (gridManager.GetCriticalMode())
                {
                    criticalCapacityBar.SetActive(false);
                    criticalCoverageBar.SetActive(true);
                }

                gridManager.SetSimulationMode("coverage");
                break;

            case 1:
                capacityBar.SetActive(true);
                coverageBar.SetActive(false);

                if (gridManager.GetCriticalMode())
                {
                    criticalCapacityBar.SetActive(true);
                    criticalCoverageBar.SetActive(false);
                }

                gridManager.SetSimulationMode("capacity");
                break;

            case 2:
                capacityBar.SetActive(false);
                coverageBar.SetActive(false);
                criticalCapacityBar.SetActive(false);
                criticalCoverageBar.SetActive(false);

                gridManager.SetSimulationMode("none");
                break;


            default:
                throw new System.Exception("This should be unreachable.");
        }
    }




}
