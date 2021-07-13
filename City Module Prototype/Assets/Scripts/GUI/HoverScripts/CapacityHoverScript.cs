using UnityEngine.EventSystems;
using UnityEngine;

public class CapacityHoverScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject capacityInfo;

    /// <summary>
    /// Displays informational text regarding the capacity bar.
    /// </summary>
    /// <param name="eventData">(Unused)</param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        capacityInfo.SetActive(true);
    }


    /// <summary>
    /// Hides informational text regarding the capacity bar.
    /// </summary>
    /// <param name="eventData">(Unused)</param>
    public void OnPointerExit(PointerEventData eventData)
    {
        capacityInfo.SetActive(false);
    }
}
