using System;
using System.Collections;
using System.Collections.Generic;
using Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class VictoryDetection : MonoBehaviour
{
    public int RequiredRobots = 1;
    public UnityEvent OnConditionAchieved;

    [SerializeField] private TMP_Text _victoryCompletionText;

    private int _currentRobots = 0;
    
    [HideInInspector] public List<RobotController> _enteredRobotControllers = new();

    private void Start()
    {
        _victoryCompletionText.text = $"{_currentRobots}/{RequiredRobots}";
        GameManager.GMInstance.onEndGame += OnEndGame;
    }

    private void OnEndGame()
    {
        _enteredRobotControllers.Clear();
        _currentRobots = 0;
        _victoryCompletionText.text = $"{_currentRobots}/{RequiredRobots}";
    }

    private void FixedUpdate()
    {
        _enteredRobotControllers.Clear();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponentInParent(out RobotController _robotController) && !_enteredRobotControllers.Contains(_robotController))
        {
            _enteredRobotControllers.Add(_robotController);
            _currentRobots++;

            _victoryCompletionText.text = $"{_currentRobots}/{RequiredRobots}";

            // TODO: Play door animation (open door, wait for .5s, close door), Destroy robot
            Debug.Log("Robot entered, do enter door animation");

            Destroy(_robotController.gameObject);

            if (_currentRobots >= RequiredRobots)
            {
                OnConditionAchieved?.Invoke();
                Debug.Log("Victory condition achieved!");
                GameManager.GMInstance.WinAndQuit();
            }
        }
    }
}
