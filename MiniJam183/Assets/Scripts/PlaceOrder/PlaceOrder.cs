using Orders;
using UnityEngine;

public class PlaceOrder : MonoBehaviour
{
    public GameObject _orderPrefab;
    public OrderScriptableObject _orderSO;

    public void OnButtonClick()
    {
        PlacementManager.Instance.StartPlacingOrder(_orderPrefab, _orderSO);
    }
}
