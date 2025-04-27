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
        public Sprite orderImage; //when in HUD

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
