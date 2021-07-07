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
        gridManager = GameObject.FindGameObjectsWithTag("Grid")[0].GetComponent<GridManager>();
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
        bool value = gridManager.ToggleCritical();
        criticalCoverageBar.SetActive(value);
        criticalCapacityBar.SetActive(value);

    }

}