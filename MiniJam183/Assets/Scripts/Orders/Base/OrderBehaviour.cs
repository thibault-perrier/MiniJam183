using System;
using System.Collections.Generic;
using System.Linq;
using Extensions;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Orders.Base
{
    public class OrderBehaviour : MonoBehaviour
    {
        public SpriteRenderer orderSpriteRenderer;
        
        [HideInInspector] public Order order;

        [HideInInspector] public List<RobotController> robotControllers = new();
        [HideInInspector] public List<RobotController> alreadyActivatedRobots = new();
        
        public float distanceThreshold = 0.3f;

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

        public void SetOrder(OrderScriptableObject _orderObject)
        {
            order = (Order)_orderObject.order.Clone();
            order.orderBehaviour = this;
            orderSpriteRenderer.sprite = order.orderIcon;
        }
        
        private void Update()
        {
            foreach (RobotController _currentRobotController in robotControllers.ToList())
            {
                if (!_currentRobotController)
                {
                    continue;
                }

                if (alreadyActivatedRobots.Contains(_currentRobotController))
                {
                    continue;
                }
                
                if (Vector2.Distance(_currentRobotController.transform.position, transform.position) > distanceThreshold)
                {
                    continue;
                }

                if (order.CanUseOrder(_currentRobotController))
                {
                    OnRobotActivateOrder(_currentRobotController);
                }
            }
        }
        
        private void OnRobotActivateOrder(RobotController _robotController)
        {
            if (order.enterCountBeforeActivation > 0)
            {
                order.enterCountBeforeActivation -= 1;
                return;
            }
            
            order.OnRobotEntered(_robotController);
            
            alreadyActivatedRobots.Add(_robotController);
        }

        private void OnTriggerEnter2D(Collider2D _other)
        {
            if (!_other.TryGetComponentInParent(out RobotController _robotController))
            {
                return;
            }
            
            if (robotControllers.Contains(_robotController))
            {
                return;
            }
            
            robotControllers.Add(_robotController);
            
        }

        private void OnTriggerExit2D(Collider2D _other)
        {
            if (!_other.TryGetComponentInParent(out RobotController _robotController))
            {
                return;
            }

            RemoveRobotController(_robotController);
        }
        
        private void RemoveRobotController(RobotController _robotController)
        {
            if (robotControllers.Contains(_robotController))
            {
                robotControllers.Remove(_robotController);
            }
            if (alreadyActivatedRobots.Contains(_robotController))
            {
                alreadyActivatedRobots.Remove(_robotController);
            }
        }
    }
}
