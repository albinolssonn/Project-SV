using UnityEngine;
using UnityEngine.UI;

public class ToggleScript : MonoBehaviour
{
    private GridManager gridManager;

    public InputField inputLimitedAntennas;
    public GameObject visualTextLimitedAntennas;
    public GameObject criticalCoverageBar;

    public void Start()
    {
        gridManager = GameObject.FindGameObjectsWithTag("Grid")[0].GetComponent<GridManager>();
    }

    public void ShowNetworkDirection()
    {
        gridManager.ToggleCreateNetworkArrows();
    }

    public void LimitedAntennas()
    {
        bool toggleCheck = gridManager.ToggleLimitedAntennas();
        inputLimitedAntennas.gameObject.SetActive(toggleCheck);
        visualTextLimitedAntennas.SetActive(!toggleCheck);
        inputLimitedAntennas.Select();
    }

    public void CriticalCoverage()
    {
        
        criticalCoverageBar.gameObject.SetActive(gridManager.ToggleCriticalCoverage());
    }
}