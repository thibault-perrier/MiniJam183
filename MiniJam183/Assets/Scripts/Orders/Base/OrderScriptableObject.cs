using Orders.Base;
using UnityEngine;

namespace Orders
{
    [CreateAssetMenu(fileName = "NewOrder", menuName = "Orders/Order")]
    public class OrderScriptableObject : ScriptableObject
    {
        [SerializeReference] public Order order;
    }
}