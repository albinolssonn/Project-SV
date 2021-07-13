using UnityEngine;
using UnityEngine.UI;

public class ToggleScript : MonoBehaviour
{
    private GridManager gridManager;

    public InputField inputLimitedAntennas;
    public GameObject visualTextLimitedAntennas;
    public GameObject criticalCoverageBar;
    public GameObject criticalCapacityBar;


    public void Start()
    {
        gridManager = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridManager>();
    }

    public void ShowNetworkDirection()
    {
        gridManager.ToggleCreateNetworkArrows();
    }

    public void LimitedAntennasMode()
    {
        bool toggleCheck = gridManager.ToggleLimitedAntennas();
        inputLimitedAntennas.gameObject.SetActive(toggleCheck);
        visualTextLimitedAntennas.SetActive(!toggleCheck);
        inputLimitedAntennas.Select();
    }

    public void CriticalMode()
    {
        bool toggle = gridManager.ToggleCritical();

        switch (gridManager.GetSimulationMode())
        {
            case "coverage":
                if (toggle)
                {
                    criticalCoverageBar.SetActive(true);
                    criticalCapacityBar.SetActive(false);
                }
                else
                {
                    criticalCoverageBar.SetActive(false);
                    criticalCapacityBar.SetActive(false);
                }

                break;

            case "capacity":
                if (toggle)
                {
                    criticalCapacityBar.SetActive(true);
                    criticalCoverageBar.SetActive(false);
                }
                else
                {
                    criticalCapacityBar.SetActive(false);
                    criticalCoverageBar.SetActive(false);
                }

                break;

            default:
                throw new System.Exception("This should be unreachable.");
        }

    }

}