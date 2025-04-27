using System;
using System.Linq;
using Extensions;
using UnityEngine;
using UnityEngine.Serialization;

public class RobotController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private LayerMask _collisionMask;
    [SerializeField] private LayerMask _climbingCollisionMask;
    [SerializeField] private LayerMask _climbableMask;
    [SerializeField] private float _speed = 2.0f;
    [SerializeField] private float _jumpForce = 5.0f;
    [SerializeField] private bool _faceRight = true;

    private Rigidbody2D _rb;
    private Animator _animator;

    public bool IsActive { get; private set; }
    public bool IsGrounded; //{ get; private set; }
    
    private bool _canJump = false;
    
    public RobotState CurrentRobotState { get; private set; } = RobotState.Walking;

    #region Climbing state var

    private bool _isClimbingUp = true;
    private bool _wasNotGroundedThisClimb = false;

    #endregion

    #region Waiting for seconds state var

    public float waitingTime = 1f;
    private RobotState waitingPreviousState;

    #endregion

    public enum RobotState
    {
        Idle,
        Walking, //normal
        Climbing, //laders / pipe
        WaitingForSeconds,
    }

    void Awake()
    {
        _rb = GetComponentInChildren<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        
    }

    public void SetLinearVelocity(Vector2 _newLinearVelocity, bool _forceSet)
    {
        SetLinearVelocity(_newLinearVelocity, _forceSet, _forceSet);
    }
    
    public void SetLinearVelocity(Vector2 _newLinearVelocity, bool _forceSetX, bool _forceSetY)
    {
        if (_forceSetX || Math.Sign(_rb.linearVelocity.x) != Math.Sign(_newLinearVelocity.x) ||
            Mathf.Abs(_newLinearVelocity.x) > Mathf.Abs(_rb.linearVelocityX))
        {
            _rb.linearVelocityX = _newLinearVelocity.x;
        }
        if (_forceSetY || Math.Sign(_rb.linearVelocity.y) != Math.Sign(_newLinearVelocity.y) ||
            Mathf.Abs(_newLinearVelocity.y) > Mathf.Abs(_rb.linearVelocityY))
        {
            _rb.linearVelocityY = _newLinearVelocity.y;
        }
    }

    public void SwitchRobotState(RobotState _nextState)
    {
        RobotState _previousState = CurrentRobotState;
        CurrentRobotState = _nextState;
        
        if (_previousState == _nextState)
        {
            return;
        }
        
        switch (_previousState)
        {
            case RobotState.Idle:
                gameObject.tag = "Untagged";
                break;
            case RobotState.Walking:
                break;
            case RobotState.Climbing:
                _rb.excludeLayers = 0;
                break;
            default:
                break;
        }
        
        switch (_nextState)
        {
            case RobotState.Idle:
                gameObject.tag = "Jumpable";
                break;
            case RobotState.Walking:
                break;
            case RobotState.Climbing:
                _rb.excludeLayers = _collisionMask;
                _wasNotGroundedThisClimb = false;
                if (!Physics2D.OverlapBox(transform.position + Vector3.up, Vector2.one, 0, _climbableMask))
                {
                    _isClimbingUp = false;
                }
                else
                {
                    _isClimbingUp = true;
                }
                break;
            case RobotState.WaitingForSeconds:
                SetLinearVelocity(new Vector2(0, _rb.linearVelocity.y), true);
                waitingPreviousState = _previousState;
                break;
            default:
                break;
        }
    }
    
    void FixedUpdate()
    {
        if (!IsActive) return;
        
        CheckIsGrounded();

        switch (CurrentRobotState)
        {
            case RobotState.Idle:
                break;
            case RobotState.Walking:
                if (!_animator.GetCurrentAnimatorStateInfo(0).IsName(IsGrounded ? "Walk" : "Jump"))
                {
                    _animator.Play(IsGrounded ? "Walk" : "Jump");
                }
                WalkingStateUpdate();
                break;
            case RobotState.Climbing:
                if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
                {
                    _animator.Play("Walk");
                }
                ClimbingStateUpdate();
                break;
            case RobotState.WaitingForSeconds:
                if (!_animator.GetCurrentAnimatorStateInfo(0).IsName(IsGrounded ? "Idle" : "Jump"))
                {
                    _animator.Play(IsGrounded ? "Idle" : "Jump");
                }
                WaitingForSecondsStateUpdate();
                break;
            default:
                break;
        }
    }

    private void WaitingForSecondsStateUpdate()
    {
        waitingTime -= Time.fixedDeltaTime;
        if (waitingTime <= 0)
        {
            SwitchRobotState(waitingPreviousState);
            waitingTime = 1f;
        }
    }

    private void WalkingStateUpdate()
    {
        SetLinearVelocity(new Vector2(transform.right.x * _speed, _rb.linearVelocity.y), false);

        var _obstacleResult = CheckObstacle();
        if (_obstacleResult)
        {
            if (_obstacleResult.collider.gameObject.CompareTag("Jumpable") && HasNoObstacleOnHead() && TryJump())
            {
                
            }
            else if(IsGrounded)
            {
                SwitchDirection();
                RaycastHit2D _otherRobot = GetObstacles().FirstOrDefault(_checkHit => _checkHit.collider.GetComponentInParent<RobotController>());
                if (_otherRobot)
                {
                    var _otherRobotController = _otherRobot.collider.GetComponentInParent<RobotController>();
                    if (_faceRight != _otherRobotController._faceRight)
                    {
                        _otherRobotController.SwitchDirection();
                    }
                    
                }
            }
        }
        
        
    }

    public bool TryJump()
    {
        if (IsGrounded)
        {
            Jump();
            return true;
        }

        return false;
    }
    
    private void Jump()
    {
        SetLinearVelocity(new Vector2(_rb.linearVelocity.x, 0), true);
        _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        _canJump = false;
    }

    private void ClimbingStateUpdate()
    {
        SetLinearVelocity(new Vector2(0, _isClimbingUp ? _speed : -_speed), true);

        bool _isClimbingGrounded = CheckIsGrounded(false, LayerMask.GetMask("Robot"));
        if (!_isClimbingGrounded)
        {
            _wasNotGroundedThisClimb = true;
        }
        
        RaycastHit2D _obstacleHit = Physics2D.Raycast(transform.position + transform.up * (_isClimbingUp ? 0.55f : -0.55f), transform.right,
            0.05f, _climbingCollisionMask);
        if (_obstacleHit.collider != null)
        {
            var _otherRobotController = _obstacleHit.collider.GetComponentInParent<RobotController>();
            if (_otherRobotController._isClimbingUp != _isClimbingUp)
            {
                _isClimbingUp = !_isClimbingUp;
                _wasNotGroundedThisClimb = true;
                _otherRobotController._isClimbingUp = !_otherRobotController._isClimbingUp;
                _otherRobotController._wasNotGroundedThisClimb = true;
            }
        }
        
        var _result = Physics2D.OverlapBox((Vector2)transform.position + _rb.linearVelocity * Time.fixedDeltaTime,
            Vector2.one * 0.1f, 0f, _climbableMask);
        if (!_result || (_isClimbingGrounded && _wasNotGroundedThisClimb))
        {
            SwitchRobotState(RobotState.Walking);
        }
    }
    
    public bool TryClimb()
    {
        if (CurrentRobotState == RobotState.Climbing)
        {
            return false;
        }
        
        if (Physics2D.OverlapBox(transform.position, Vector2.one * 1f, 0f, _climbableMask))
        {
            SwitchRobotState(RobotState.Climbing);
            return true;
        }
        return false;
    }
    
    private RaycastHit2D CheckObstacle()
    {
        //RaycastHit2D _hit = Physics2D.Raycast(transform.position + transform.right* 0.55f , transform.right, 0.05f, _collisionMask);
        var _hits = GetObstacles();

        RaycastHit2D _hit = _hits.FirstOrDefault(_checkHit => (!_checkHit.collider.attachedRigidbody || _checkHit.collider.attachedRigidbody != _rb));
        
        if (_hit.collider)
        {
            //Debug.Log($"Hit detected: {hit.collider.gameObject.name}");
            if (_hit.collider.gameObject.CompareTag("Jumpable"))
                _canJump = true;
            else
                _canJump = false;

            if (_hit.collider.gameObject.name != transform.gameObject.name)
            {
                return _hit;
            }
        }

        return default;
    }

    private RaycastHit2D[] GetObstacles()
    {
        float _size = 0.9f;
        RaycastHit2D[] _hits = Physics2D.BoxCastAll(transform.position + transform.right * (1 - _size), Vector2.one * _size,
            0f, transform.right, 0.05f, _collisionMask);
        DebugRaycast(transform.position + transform.right * 0.55f, transform.right, 0.05f, Color.blue);
        return _hits;
    }

    public void SwitchDirection()
    {
        if (CurrentRobotState == RobotState.Climbing)
        {
            _isClimbingUp = !_isClimbingUp;
            return;
        }
        _faceRight = !_faceRight;
        transform.rotation = Quaternion.Euler(0, _faceRight ? 0 : 180, 0);
        Debug.Log("switching");
       // _rb.linearVelocity = transform.right * _speed;
    }

    private  bool HasNoObstacleOnHead()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + transform.up, transform.right, 1f,
            _collisionMask);
        DebugRaycast(transform.position + transform.up , transform.right, 1f, Color.red);

        if (hit.collider)
            return false;
        
        return true;
    }
    
    private bool CheckIsGrounded(bool _updateGroundedValue = true, LayerMask _excludeLayer = default)
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position + -transform.up  * 0.5f, Vector2.down, 0.05f,
            _collisionMask & ~_excludeLayer);
        
        DebugRaycast(transform.position + -transform.up  * 0.5f, Vector2.down, 0.05f, Color.red);
        
        RaycastHit2D hit = hits
        .FirstOrDefault(hit => (!hit.collider.attachedRigidbody || hit.collider.attachedRigidbody != _rb));

        bool _newValue = hit.collider;
        if (_updateGroundedValue)
        {
            IsGrounded = _newValue;
        }
        return _newValue;
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
    private void DebugRaycast(Vector3 origin, Vector3 direction, float distance, Color color)
    {
        Debug.DrawRay(origin, direction * distance, color);
    }
    void OnValidate()
    {
        transform.rotation = Quaternion.Euler(0, _faceRight ? 0 : 180, 0);
    }
#endif
    
}
