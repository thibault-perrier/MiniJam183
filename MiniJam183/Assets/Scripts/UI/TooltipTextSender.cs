using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTextSender : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipOrder.TOInstance.ShowTooltip();
        TooltipOrder.TOInstance.SetTooltipText(GetComponentInChildren<PlaceOrder>()._orderSO.order.orderDescription);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipOrder.TOInstance.HideTooltip();
    }
}
