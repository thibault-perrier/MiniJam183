using Orders.Base;
using UnityEngine;

namespace Orders
{
    public class OrderWaitForSecond : Order
    {
        public float waitingTime = 1f;
        
        public override void OnRobotEntered(RobotController _robotController)
        {
            base.OnRobotEntered(_robotController);
            _robotController.SwitchRobotState(RobotController.RobotState.WaitingForSeconds);
            _robotController.waitingTime = waitingTime;
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