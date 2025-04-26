using Orders;
using Orders.Base;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlacementManager : MonoBehaviour
{
    public static PlacementManager Instance;

    private GameObject _orderPrefabToPlace;
    private OrderScriptableObject _currentOrderSO;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (_orderPrefabToPlace !=null && Input.GetMouseButtonDown(0))
        {
            // Ignore if clicking on UI
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            GameObject placedOrder = Instantiate(_orderPrefabToPlace, mousePos, Quaternion.identity);
            OrderBehaviour orderBehaviour = placedOrder.GetComponent<OrderBehaviour>();
            if (orderBehaviour != null)
            {
                orderBehaviour.SetOrder(_currentOrderSO);
            }

            _orderPrefabToPlace = null;
            _currentOrderSO = null;
        }
    }

    public void StartPlacingOrder(GameObject prefab, OrderScriptableObject orderSO)
    {
        _orderPrefabToPlace = prefab;
        _currentOrderSO = orderSO;
    }
}
