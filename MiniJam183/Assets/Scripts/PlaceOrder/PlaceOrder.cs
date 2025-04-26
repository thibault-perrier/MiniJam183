using Orders;
using UnityEngine;

public class PlaceOrder : MonoBehaviour
{
    public GameObject _orderPrefab;
    public OrderScriptableObject _orderSO;

    public void OnPlace()
    {
        PlacementManager.Instance.StartPlacingOrder(_orderPrefab, _orderSO);
    }
}
