using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager GMInstance;

    public bool IsInGameMode = false;

    public event Action onStartGame;
    public event Action onEndGame;

    [SerializeField] private GameObject _victoryText;
    [SerializeField] private AnimationClip _fadeOutClip;

    [SerializeField] private float _timeBeforeFadeOut = 1.0f;
    [SerializeField] private Animator _blackScreenExit;

    public List<RobotController> aliveRobots = new();

    private void Awake()
    {
        if (GMInstance == null)
        {
            GMInstance = this;
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
        IsInGameMode = true;
        onStartGame?.Invoke();
    }

    /// <summary>
    /// This method must be called when all the robots are dead or the stop button is pressed
    /// </summary>
    public void EndGame()
    {
        DestroyAllRobots();
        IsInGameMode = false;
        aliveRobots.Clear();
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

    public void WinAndQuit()
    {
        _victoryText.SetActive(true);
        StartCoroutine(ExitAfterTime());
    }

    public void QuitGame()
    {
        LoadMainMenu();
    }

    private IEnumerator ExitAfterTime()
    {
        yield return new WaitForSecondsRealtime(_timeBeforeFadeOut);
        _blackScreenExit.SetTrigger("FadeOut");
        yield return new WaitForSecondsRealtime(_fadeOutClip.length);
        LoadMainMenu();
    }

    public void LoadMainMenu()
    {
        MainMenu.HasToLoadLevelSelector = true;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}