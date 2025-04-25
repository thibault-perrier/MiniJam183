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
    private Vector3 _dir = Vector3.right;

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

        _rb.linearVelocity = new Vector2(_dir.x * _speed, _rb.linearVelocity.y);

        if (CheckObstacle())
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
        RaycastHit2D hit = Physics2D.Raycast(transform.position + _dir / 2, _dir, 0.1f, _collisionMask);
        DebugRaycast();

        if (hit.collider != null && hit.collider.gameObject != gameObject)
        {
            Debug.Log($"Hit detected: {hit.collider.gameObject.name}");
            return hit.collider.gameObject != transform.root.gameObject;
        }

        return false;
    }

    private void DebugRaycast()
    {
        Debug.DrawRay(transform.position + _dir / 2, _dir * 0.1f, Color.red);
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
        _spriteRenderer.flipX = !_spriteRenderer.flipX;
        _dir = -_dir;

        _rb.linearVelocity = _dir * _speed;
    }


    void OnValidate()
    {
        _dir = _faceRight ? Vector3.right : Vector3.left;
        if (_spriteRenderer != null)
        {
            _spriteRenderer.flipX = !_faceRight;
        }
    }

}
