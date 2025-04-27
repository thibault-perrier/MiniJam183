using Orders.Base;

namespace Orders
{
    public class OrderSwitchDirection : Order
    {
        public override void OnRobotEntered(RobotController _robotController)
        {
            base.OnRobotEntered(_robotController);
            _robotController.SwitchDirection();
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