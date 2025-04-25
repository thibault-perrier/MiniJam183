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

        [Header("Values")]
        [HideInInspector] public OrderBehaviour orderBehaviour;
        [HideInInspector] public int orderUseCount = int.MaxValue;
        [HideInInspector] public int enterCountBeforeActivation = 0;
        

        public virtual void OnRobotEntered(Object _robot)
        {
            orderUseCount -= 1;
            if (orderUseCount <= 0)
            {
                orderBehaviour.gameObject.SetActive(false);
            }
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
