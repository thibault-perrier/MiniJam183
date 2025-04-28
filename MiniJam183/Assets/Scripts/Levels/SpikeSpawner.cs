using UnityEngine;

public class SpikeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _spikePrefab;

    [SerializeField] private float _spawnCycleTimer = 3.0f;
    [SerializeField] private float _firstSpawnDelay = 0.0f;
    private float _currTimer = 0.0f;

    private void Start()
    {
        GameManager.GMInstance.onStartGame += SetupValues;
    }

    private void SetupValues()
    {
        SpawnSpike();
        _currTimer = -_firstSpawnDelay;
        Debug.Log($"Spike Spawner Setup Values: {_currTimer}");
    }

    void Update()
    {
        if (!GameManager.GMInstance.IsInGameMode) return;

        _currTimer += Time.deltaTime;

        if (_currTimer >= _spawnCycleTimer)
        {
            SpawnSpike();
            _currTimer = 0.0f;
        }
    }



    private void SpawnSpike()
    {
        Debug.Log("Spawn Spike");
        FallingSpike _spikeObject = Instantiate(_spikePrefab, new Vector3(transform.position.x, transform.position.y - 1.0f, transform.position.z), Quaternion.identity, transform).GetComponent<FallingSpike>();
    }
}
