using UnityEngine.EventSystems;
using UnityEngine;

public class CriticalCapacityHoverScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject criticalCapacityInfo;

    public void OnPointerEnter(PointerEventData eventData)
    {
        criticalCapacityInfo.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        criticalCapacityInfo.SetActive(false);
    }
}
