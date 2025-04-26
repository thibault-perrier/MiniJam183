using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActivateRobots : MonoBehaviour
{
    List<RobotController> _robots = new List<RobotController>();

    public void ActivateAllRobots()
    {
        _robots = GetComponentsInChildren<RobotController>().ToList();
        foreach (var robot in _robots)
        {
            robot.ActivateRobot();
        }
    }
}
