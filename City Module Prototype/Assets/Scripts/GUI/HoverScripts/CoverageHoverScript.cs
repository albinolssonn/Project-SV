using UnityEngine.EventSystems;
using UnityEngine;

public class CoverageHoverScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject coverageInfo;

    /// <summary>
    /// Displays informational text regarding the coverage bar.
    /// </summary>
    /// <param name="eventData">(Unused)</param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        coverageInfo.SetActive(true);
    }


    /// <summary>
    /// Hides informational text regarding the coverage bar.
    /// </summary>
    /// <param name="eventData">(Unused)</param>
    public void OnPointerExit(PointerEventData eventData)
    {
        coverageInfo.SetActive(false);
    }
}
