using Orders.Base;
using UnityEngine;

public class OrderTurnOff : Order
{
    public override void OnRobotEntered(RobotController _robotController)
    {
        base.OnRobotEntered(_robotController);
        _robotController.DeactivateRobot();
    }
}
