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
    }
}