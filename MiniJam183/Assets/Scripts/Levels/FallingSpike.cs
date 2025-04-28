using Extensions;
using Orders.Base;
using UnityEngine;

public class FallingSpike : MonoBehaviour
{
    [SerializeField] private GameObject _destructionParticles;
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

        if (_other.GetComponentInParent<OrderBehaviour>() != null)
        {
            return;
        }

        Instantiate(_destructionParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
