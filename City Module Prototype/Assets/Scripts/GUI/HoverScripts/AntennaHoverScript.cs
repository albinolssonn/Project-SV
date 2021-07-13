using UnityEngine.EventSystems;
using UnityEngine;

public class AntennaHoverScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject antennaInfo;


    /// <summary>
    /// Displays informational text regarding the Antenna counter.
    /// </summary>
    /// <param name="eventData">(Unused)</param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        antennaInfo.SetActive(true);
    }


    /// <summary>
    /// Hides informational text regarding the Antenna counter.
    /// </summary>
    /// <param name="eventData">(Unused)</param>
    public void OnPointerExit(PointerEventData eventData)
    {
        antennaInfo.SetActive(false);
    }
}