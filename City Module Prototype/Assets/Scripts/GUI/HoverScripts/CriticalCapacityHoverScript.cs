using UnityEngine.EventSystems;
using UnityEngine;

public class CriticalCapacityHoverScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject criticalCapacityInfo;


    /// <summary>
    /// Displays informational text regarding the critical capacity bar.
    /// </summary>
    /// <param name="eventData">(Unused)</param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        criticalCapacityInfo.SetActive(true);
    }


    /// <summary>
    /// Hides informational text regarding the critical capacity bar.
    /// </summary>
    /// <param name="eventData">(Unused)</param>
    public void OnPointerExit(PointerEventData eventData)
    {
        criticalCapacityInfo.SetActive(false);
    }
}
