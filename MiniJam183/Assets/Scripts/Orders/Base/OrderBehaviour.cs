using System;
using UnityEngine;

namespace Orders.Base
{
    public class OrderBehaviour : MonoBehaviour
    {
        [HideInInspector] public Order order;

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
            //     order.orderBehaviour = this;
            //     order.OnRobotEntered(robot);
            // }
        }
    }
}
