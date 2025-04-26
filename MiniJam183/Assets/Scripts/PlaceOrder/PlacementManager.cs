using Orders;
using Orders.Base;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    public static PlacementManager Instance;

    private OrderBehaviour _orderBehaviour;
    private SpriteRenderer _orderImage;

    [Tooltip("Mask for the layers that can be overlapped by the order prefab")]
    [SerializeField] private LayerMask _overlappingLayerMask;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (_orderBehaviour == null)
            return;

        UpdatePreview();

        if (Input.GetMouseButtonDown(0))
        {
            if (_orderBehaviour.CanBePlaced)
            {
                _orderBehaviour = null;
                _orderImage = null;
            }
            else
                Debug.Log("Cannot place order here, please try again.");
        }
    }

    private void UpdatePreview()
    {
        _orderImage.color = _orderBehaviour.CanBePlaced ? Color.white : Color.red;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _orderBehaviour.transform.position = new Vector2(Mathf.FloorToInt(mousePos.x) + 0.5f, Mathf.FloorToInt(mousePos.y) + 0.5f);
    }


    public void StartPlacingOrder(GameObject prefab, OrderScriptableObject orderSO)
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _orderBehaviour = Instantiate(prefab, new Vector2(Mathf.FloorToInt(mousePos.x) + 0.5f, Mathf.FloorToInt(mousePos.y) + 0.5f), Quaternion.identity).GetComponent<OrderBehaviour>();

        if (_orderBehaviour != null)
        {
            _orderBehaviour.SetOrder(orderSO);
            _orderImage = _orderBehaviour.GetComponentInChildren<SpriteRenderer>();
        }
    }
}
