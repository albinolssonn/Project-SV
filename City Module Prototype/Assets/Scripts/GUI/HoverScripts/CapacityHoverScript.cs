using UnityEngine.EventSystems;
using UnityEngine;

public class CapacityHoverScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject capacityInfo;

    public void OnPointerEnter(PointerEventData eventData)
    {
        capacityInfo.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        capacityInfo.SetActive(false);
    }
}
