using UnityEngine.EventSystems;
using UnityEngine;

public class CriticalCoverageHoverScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject criticalCoverageInfo;

    public void OnPointerEnter(PointerEventData eventData)
    {
        criticalCoverageInfo.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        criticalCoverageInfo.SetActive(false);
    }
}

