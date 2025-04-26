using System;
using System.Linq;
using Orders;
using Orders.Base;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    public static PlacementManager Instance;

    private OrderBehaviour _orderBehaviour;
    private SpriteRenderer _orderImage;
    private bool _canBePlaced = true;
    private bool _wasOnOrderButton = false;

    [Tooltip("Mask for the layers that can be overlapped by the order prefab")]
    [SerializeField] private LayerMask _overlappingLayerMask;

    private void Awake()
    {
        Instance = this;
    }

    private void ReleaseCurrentOrder()
    {
        if (_orderBehaviour != null)
        {
            Destroy(_orderBehaviour.gameObject);
            _orderBehaviour = null;
            _orderImage = null;
            _canBePlaced = false;
        }
    }

    private void Update()
    {
        if (!_wasOnOrderButton && UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            ReleaseCurrentOrder();
        else if (_wasOnOrderButton && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            _wasOnOrderButton = false;

        if (GameManager.GMInstance.IsInGameMode)
            return;

        if (Input.GetMouseButtonDown(1))
        {
            if (_orderBehaviour != null)
            {
                ReleaseCurrentOrder();
                return;
            }


            Debug.Log("Right click to delete order");
            OrderBehaviour orderToDelete =
                Physics2D.OverlapCircleAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.2f)
                        .Select(c => c.GetComponentInParent<OrderBehaviour>())
                        .FirstOrDefault();
            if (orderToDelete != null)
            {
                Destroy(orderToDelete.gameObject);
                return;
            }
        }

        if (_orderBehaviour == null)
            return;

        CheckCanBePlaced();
        UpdatePreview();

        if (Input.GetMouseButtonDown(0))
        {
            if (_canBePlaced)
            {
                _orderBehaviour = null;
                _orderImage = null;
                _canBePlaced = false;
            }
            else
                Debug.Log("Cannot place order here, please try again.");
        }
    }

    private void CheckCanBePlaced()
    {
        Collider2D[] res = Physics2D.OverlapBoxAll(_orderBehaviour.transform.position, _orderBehaviour.GetComponentInChildren<BoxCollider2D>().size, 0.0f);
        _canBePlaced =
            !res.Any(c => c.GetComponentInParent<OrderBehaviour>() != null && c.GetComponentInParent<OrderBehaviour>() != _orderBehaviour) &&
            Physics2D.OverlapBox(_orderBehaviour.transform.position, _orderBehaviour.GetComponentInChildren<BoxCollider2D>().size, 0.0f, _overlappingLayerMask) == null;
    }

    private void UpdatePreview()
    {
        _orderImage.color = _canBePlaced ? Color.white : Color.red;
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
            _wasOnOrderButton = true;
        }
    }
}
