
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public event Action onStartGame;
    public event Action onEndGame;
    
    public List<RobotController> aliveRobots = new();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    /// <summary>
    /// This method must be called when the "start" button is pressed after placing orders
    /// </summary>
    public void StartGame()
    {
        onStartGame?.Invoke();
    }
    
    /// <summary>
    /// This method must be called when all the robots are dead or the stop button is pressed
    /// </summary>
    public void EndGame()
    {
        aliveRobots.Clear();
        onEndGame?.Invoke();
    }
}