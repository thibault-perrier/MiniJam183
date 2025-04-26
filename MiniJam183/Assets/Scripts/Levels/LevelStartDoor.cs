using System;
using System.Collections;
using UnityEngine;

public class LevelStartDoor : MonoBehaviour
{
    public int spawnCount = 3;
    public float spawnDelay = 1f;
    public bool faceRightOnSpawn = true;
    
    public GameObject robotPrefab;
    
    private Coroutine spawningCoroutine;

    private void Start()
    {
        GameManager.instance.onStartGame += StartSpawning;
        GameManager.instance.onEndGame += StopSpawning;
    }

    [ContextMenu("Spawn")]
    public void StartSpawning()
    {
        spawningCoroutine = StartCoroutine(SpawningCoroutine());
    }

    public void StopSpawning()
    {
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
        
        GameManager.instance.aliveRobots.Add(_robotController);
    }
}
