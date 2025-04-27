using Extensions;
using Levels;
using UnityEngine;

public class FallingSpike : MonoBehaviour
{
    [SerializeField] private float _spikeSpeed = 5.0f;

    void Update()
    {
        transform.position += Vector3.down * (Time.deltaTime * _spikeSpeed);
    }

    public void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.TryGetComponentInParent(out RobotController _robotController))
        {
            _robotController.KillRobot();
        }

        Debug.Log("Destroy Spike");
        Destroy(gameObject);
    }
}
