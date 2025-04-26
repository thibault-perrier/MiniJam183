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
            _robotController.TryJump();
            _robotController.GetComponent<Rigidbody2D>().AddForce(_robotController.transform.right * speedBoost, ForceMode2D.Impulse);
            orderBehaviour.StartCoroutine(WaitForGroundedToRemoveBoost(_robotController));

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
