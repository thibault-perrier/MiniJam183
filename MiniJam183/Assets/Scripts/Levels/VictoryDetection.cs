using System.Collections;
using Extensions;
using UnityEngine;
using UnityEngine.Events;

public class VictoryDetection : MonoBehaviour
{
    public int RequiredRobots = 1;
    public UnityEvent OnConditionAchieved;

    private int _currentRobots = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponentInParent(out RobotController _robotController))
        {
            _currentRobots++;

            // TODO: Play door animation (open door, wait for .5s, close door), Destroy robot
            Debug.Log("Robot entered, do enter door animation");

            Destroy(_robotController.gameObject);

            if (_currentRobots >= RequiredRobots)
            {
                OnConditionAchieved?.Invoke();
                Debug.Log("Victory condition achieved!");
            }
        }
    }
}
