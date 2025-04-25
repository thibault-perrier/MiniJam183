using System;
using UnityEngine;

namespace Orders.Base
{
    public class OrderBehaviour : MonoBehaviour
    {
        [HideInInspector] public Order order;

        
        // TODO: CALL WHEN ORDER PLACED IN GAME
        public void SetOrder(OrderScriptableObject _orderObject)
        {
            order = (Order)_orderObject.order.Clone();
            order.orderBehaviour = this;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            // TODO: 
            // if (other.TryGetComponent<Robot>(out var robot))
            // {
            //     if (order.enterCountBeforeActivation > 0)
            //     {
            //         order.enterCountBeforeActivation -= 1;
            //         return;
            //     }
            //     order.OnRobotEntered(robot);
            // }
        }
    }
}
