using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Orders.Base
{
    public abstract class Order : ICloneable
    {
        [Header("Description")]
        public string orderName;
        public string orderDescription;
        public Sprite orderIcon; //when placed

        [Header("Values")]
        [HideInInspector] public OrderBehaviour orderBehaviour;
        [HideInInspector] public int orderUseCount = int.MaxValue;
        [HideInInspector] public int enterCountBeforeActivation = 0;
        

        public virtual void OnRobotEntered(RobotController _robotController)
        {
            orderUseCount -= 1;
            if (orderUseCount <= 0)
            {
                orderBehaviour.gameObject.SetActive(false);
            }
            _robotController.transform.position = orderBehaviour.transform.position;
        }

        public virtual bool CanUseOrder(RobotController _robotController)
        {
            if (_robotController.CurrentRobotState is RobotController.RobotState.Off or RobotController.RobotState.WaitingForSeconds)
            {
                return false;
            }

            return true;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
