using UnityEngine;

public class RobotController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private LayerMask _collisionMask;
    [SerializeField] private float _speed = 2.0f;
    [SerializeField] private bool _faceRight = true;

    private Rigidbody2D _rb;

    public bool IsActive { get; private set; }
    public bool IsGrounded { get; private set; }

    private bool _needToSwitchDirection = false;
    private bool _waitingForNextFrame = false;



    void Awake()
    {
        _rb = GetComponentInChildren<Rigidbody2D>();
    }

    void Start()
    {
        IsActive = true;
    }

    void Update()
    {
        if (!IsActive) return;

        _rb.linearVelocity = new Vector2(transform.right.x * _speed, _rb.linearVelocity.y);

        
        if (!_needToSwitchDirection && CheckObstacle())
        {
            _needToSwitchDirection = true;
            _waitingForNextFrame = true;
        }

        if (_needToSwitchDirection && !_waitingForNextFrame)
        {
            _needToSwitchDirection = false;
            SwitchDirection();
        }
        else
            _waitingForNextFrame = false;
    }

    private bool CheckObstacle()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + transform.right* 0.6f , transform.right, 0.05f, _collisionMask);
        DebugRaycast();

        if (hit.collider != null )//&& hit.collider.gameObject != gameObject)
        {
            Debug.Log($"Hit detected: {hit.collider.gameObject.name}");
            return hit.collider.gameObject.name != transform.gameObject.name;
        }

        // if (hit.collider != null && hit.collider.gameObject != gameObject)
        // {
        //     return true;
        // }
        return false;
    }

    private void DebugRaycast()
    {
        Debug.DrawRay(transform.position + transform.right / 2, transform.right * 0.1f, Color.red);
    }

    [ContextMenu("Activate Robot")]
    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void SwitchDirection()
    {
        _faceRight = !_faceRight;
        transform.rotation = Quaternion.Euler(0, _faceRight ? 0 : 180, 0);
        _rb.linearVelocity = transform.right * _speed;
    }


    void OnValidate()
    {
        transform.rotation = Quaternion.Euler(0, _faceRight ? 0 : 180, 0);
    }

}
