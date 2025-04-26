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
    }
}