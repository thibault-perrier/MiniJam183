using System.Collections;
using Orders.Base;
using UnityEngine;

namespace Orders
{
    public class OrderJump : Order
    {
        public float speedBoost = 1.25f;
        
        public override void OnRobotEntered(RobotController _robotController)
        {
            base.OnRobotEntered(_robotController);
            if (_robotController.TryJump())
            {
                _robotController.SetLinearVelocity( 
                    new Vector2(_robotController.transform.right.x * (_robotController.Speed + speedBoost), _robotController.Rb.linearVelocityY), 
                    true, false);
                orderBehaviour.StartCoroutine(WaitForGroundedToRemoveBoost(_robotController));
            }
        }
        
        public override bool CanUseOrder(RobotController _robotController)
        {
            if (base.CanUseOrder(_robotController) == false)
            {
                return false;
            }

            if (!_robotController.IsGrounded)
            {
                return false;
            }
            
            return true;
        }

        private IEnumerator WaitForGroundedToRemoveBoost(RobotController _robotController)
        {
            while (true)
            {
                if (!_robotController.IsGrounded)
                {
                    break;
                }

                yield return null;
            }
            
            while (true)
            {
                if (_robotController.IsGrounded)
                {
                    var _rigidbody2D = _robotController.GetComponent<Rigidbody2D>();
                    _rigidbody2D.linearVelocity = new Vector2(0, _rigidbody2D.linearVelocityY);
                    yield break;
                }
                yield return null;
            }
        }
    }
}
