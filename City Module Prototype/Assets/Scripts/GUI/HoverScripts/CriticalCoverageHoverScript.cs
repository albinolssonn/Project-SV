using UnityEngine.EventSystems;
using UnityEngine;

public class CriticalCoverageHoverScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject criticalCoverageInfo;


    /// <summary>
    /// Displays informational text regarding the critical coverage bar.
    /// </summary>
    /// <param name="eventData">(Unused)</param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        criticalCoverageInfo.SetActive(true);
    }


    /// <summary>
    /// Hides the informational text regarding the critical coverage bar.
    /// </summary>
    /// <param name="eventData">(Unused)</param>
    public void OnPointerExit(PointerEventData eventData)
    {
        criticalCoverageInfo.SetActive(false);
    }
}

