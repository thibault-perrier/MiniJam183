using System;
using UnityEngine;

namespace Orders.Base
{
    public class OrderBehaviour : MonoBehaviour
    {
        [HideInInspector] public Order order;

        private void Start()
        {
            //TEST
            // order = new OrderJump();
            // order.orderBehaviour = this;
        }

        [ContextMenu("Test Add Switch Order")]
        private void TestAddSwitchOrder()
        {
            order = new OrderSwitchDirection();
            order.orderBehaviour = this;
        }
        
        [ContextMenu("Test Add Climb Order")]
        private void TestAddClimbOrder()
        {
            order = new OrderClimb();
            order.orderBehaviour = this;
        }
        
        // TODO: CALL WHEN ORDER PLACED IN GAME
        public void SetOrder(OrderScriptableObject _orderObject)
        {
            order = (Order)_orderObject.order.Clone();
            order.orderBehaviour = this;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            RobotController _robotController = other.GetComponentInParent<RobotController>();
            if (!_robotController)
            {
                return;
            }
            
            if (order.enterCountBeforeActivation > 0)
            {
                order.enterCountBeforeActivation -= 1;
                return;
            }
            order.OnRobotEntered(_robotController);
        }
    }
}
