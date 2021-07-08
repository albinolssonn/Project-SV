using UnityEngine.EventSystems;
using UnityEngine;

public class CoverageHoverScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject coverageInfo;

    public void OnPointerEnter(PointerEventData eventData)
    {
        coverageInfo.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        coverageInfo.SetActive(false);
    }
}
