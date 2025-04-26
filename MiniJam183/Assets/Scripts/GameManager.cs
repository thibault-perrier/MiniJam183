
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public event Action onStartGame;
    public event Action onEndGame;

    [SerializeField] private float _timeBeforeFadeOut = 1.0f;
    [SerializeField] private Animator _blackScreenExit;

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

        _blackScreenExit.gameObject.SetActive(true);
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
        DestroyAllRobots();
        onEndGame?.Invoke();
    }

    public void DestroyAllRobots()
    {
        foreach (var robot in aliveRobots)
        {
            if (robot == null) continue;
            Destroy(robot.gameObject);
        }
        aliveRobots.Clear();
    }

    public void QuitLevel()
    {
        StartCoroutine(ExitAfterTime());
    }

    private IEnumerator ExitAfterTime()
    {
        yield return new WaitForSeconds(_timeBeforeFadeOut);
        _blackScreenExit.SetTrigger("FadeOut");
    }
}