using Orders.Base;
using UnityEngine;

public class OrderTurnOff : Order
{
    public override void OnRobotEntered(RobotController _robotController)
    {
        base.OnRobotEntered(_robotController);
        _robotController.SwitchRobotState(RobotController.RobotState.Off);
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
