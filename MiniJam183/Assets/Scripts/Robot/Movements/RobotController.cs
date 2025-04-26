using UnityEngine;

public class RobotController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private LayerMask _collisionMask;
    [SerializeField] private float _speed = 2.0f;
    [SerializeField] private bool _faceRight = true;

    private Rigidbody2D _rb;

    public bool IsActive { get; private set; }
    public bool IsGrounded; //{ get; private set; }

    private bool _needToSwitchDirection = false;
    private bool _waitingForNextFrame = false;
    
    private bool _canJump = false;

    void Awake()
    {
        _rb = GetComponentInChildren<Rigidbody2D>();
    }

    void Start()
    {

    }

    void Update()
    {
        if (!IsActive) return;
        
        CheckIsGrounded();

        _rb.linearVelocity = new Vector2(transform.right.x * _speed, _rb.linearVelocity.y);
        CanJump();
        if (!_needToSwitchDirection && CheckObstacle())
        {
            if (CanJump() && IsGrounded && _canJump) 
            {
                 _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, 0);
                _rb.AddForce(Vector2.up * 4.5f, ForceMode2D.Impulse);
                _canJump = false;
            }
            else if(IsGrounded)
            {
                _needToSwitchDirection = true;
                _waitingForNextFrame = true;
            }
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
        RaycastHit2D hit = Physics2D.Raycast(transform.position + transform.right* 0.55f , transform.right, 0.05f, _collisionMask);
        DebugRaycast(transform.position + transform.right * 0.55f, transform.right, 0.05f);

        if (hit.collider != null)
        {
            //Debug.Log($"Hit detected: {hit.collider.gameObject.name}");
            if (hit.collider.gameObject.CompareTag("Jumpable"))
                _canJump = true;
            else
                _canJump = false;
            
            return hit.collider.gameObject.name != transform.gameObject.name;
        }

        return false;
    }

    public void SwitchDirection()
    {
        _faceRight = !_faceRight;
        transform.rotation = Quaternion.Euler(0, _faceRight ? 0 : 180, 0);
       // _rb.linearVelocity = transform.right * _speed;
    }

    private  bool CanJump()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + transform.up, transform.right, 1f,
            _collisionMask);
        DebugRaycast(transform.position + transform.up , transform.right, 1f);

        if (hit.collider)
            return false;
        
        return true;
    }
    
    private void CheckIsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + -transform.up  * 0.6f, Vector2.down, 0.1f,
            _collisionMask);
        
        DebugRaycast(transform.position + -transform.up * 0.6f, Vector2.down, 0.1f);
        
        IsGrounded = hit.collider;
    }
    
    
#if UNITY_EDITOR
    [ContextMenu("Activate Robot")]
#endif
    public void ActivateRobot()
    {
        IsActive = true;
    }

    public void DeactivateRobot()
    {
        IsActive = false;
    }
    
    
#if UNITY_EDITOR
    private void DebugRaycast(Vector3 origin, Vector3 direction, float distance)
    {
        Debug.DrawRay(origin, direction * distance, Color.red);
    }
    void OnValidate()
    {
        transform.rotation = Quaternion.Euler(0, _faceRight ? 0 : 180, 0);
    }
#endif
    
}
