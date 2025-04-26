using UnityEngine;
using UnityEngine.Events;

public class VictoryDetection : MonoBehaviour
{
    public UnityEvent OnConditionAchieved;
    public int RequiredRobots = 1;

    private int _currentRobots = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Robot"))
        {
            _currentRobots++;

            // TODO: Play door animation (open door, wait for .5s, close door), Destroy robot
            Debug.Log("Robot entered, do enter door animation");

            Destroy(other.gameObject);

            if (_currentRobots >= RequiredRobots)
            {
                OnConditionAchieved?.Invoke();
                Debug.Log("Victory condition achieved!");
            }
        }
    }
}
