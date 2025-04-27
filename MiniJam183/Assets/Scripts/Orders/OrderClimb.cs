using Orders.Base;
using UnityEngine;

namespace Orders
{
    public class OrderClimb : Order
    {
        public override void OnRobotEntered(RobotController _robotController)
        {
            base.OnRobotEntered(_robotController);
            if (_robotController.TryClimb())
            {
                _robotController.transform.position = orderBehaviour.transform.position;
            }
        }
        
        public override bool CanUseOrder(RobotController _robotController)
        {
            if (base.CanUseOrder(_robotController) == false)
            {
                return false;
            }
            return true;
        }
    }
}