using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class LevelStartDoor : MonoBehaviour
{
    [SerializeField] private TMP_Text _currentRobotBehindDoorText;

    public int spawnCount = 3;
    public float spawnDelay = 1f;
    public bool faceRightOnSpawn = true;

    public GameObject robotPrefab;

    private Coroutine spawningCoroutine;

    private void Start()
    {
        GameManager.GMInstance.onStartGame += StartSpawning;
        GameManager.GMInstance.onEndGame += StopSpawning;

        _currentRobotBehindDoorText.text = $"{spawnCount}/{spawnCount}";
    }

    [ContextMenu("Spawn")]
    public void StartSpawning()
    {
        _currentRobotBehindDoorText.text = $"{spawnCount}/{spawnCount}";
        spawningCoroutine = StartCoroutine(SpawningCoroutine());
    }

    public void StopSpawning()
    {
        _currentRobotBehindDoorText.text = $"{spawnCount}/{spawnCount}";

        if (spawningCoroutine != null)
        {
            StopCoroutine(spawningCoroutine);
        }
    }

    private IEnumerator SpawningCoroutine()
    {
        for (int _i = 0; _i < spawnCount; _i++)
        {
            while (true)
            {
                if (Physics2D.OverlapBox(transform.position, Vector2.one, 0f, LayerMask.GetMask("Robot")) == null)
                {
                    break;
                }
                yield return null;
            }
            SpawnRobot();
            _currentRobotBehindDoorText.text = $"{spawnCount - (_i + 1)}/{spawnCount}";
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private void SpawnRobot()
    {
        var _robotObject = Instantiate(robotPrefab, transform.position, Quaternion.identity);
        var _robotController = _robotObject.GetComponentInChildren<RobotController>();
        _robotController.ActivateRobot();
        if (!faceRightOnSpawn)
        {
            _robotController.SwitchDirection();
        }

        GameManager.GMInstance.aliveRobots.Add(_robotController);
    }
}
