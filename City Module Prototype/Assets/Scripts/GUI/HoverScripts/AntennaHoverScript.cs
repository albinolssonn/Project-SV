using UnityEngine.EventSystems;
using UnityEngine;

public class AntennaHoverScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject antennaInfo;

    public void OnPointerEnter(PointerEventData eventData)
    {
        antennaInfo.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        antennaInfo.SetActive(false);
    }
}