using Orders.Base;
using UnityEngine;

namespace Orders
{
    public class OrderJump : Order
    {
        public override void OnRobotEntered(RobotController _robotController)
        {
            base.OnRobotEntered(_robotController);
            _robotController.TryJump();
        }
    }
}
