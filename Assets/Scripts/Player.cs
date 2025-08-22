using UnityEngine;

public class Player : Entity
{
    [Header("State Machine")]
    public Player_IdleState IdleState { get; private set; }
    public Player_MoveState MoveState { get; private set; }
    public Player_AirState AirState { get; private set; }
    public Player_JumpState JumpState { get; private set; }
    public Player_FallState FallState { get; private set; }

    [Header("Player Component")]
    public Rigidbody2D Rb;

    [Header("InputSystem")]
    public PlayerInput InputSystem;
    public float JumpWindow = 0.2f;
    public float JumpDelay = 0.06f;
    public float JumpGravity = 3f;
    public float FallGravity = 5f;

    [Header("Player Attribute")]
    public float MoveSpeed = 1f;
    public float MoveSpeedAir = 0.8f;
    public float JumpHeight = 3f;
    public float JumpHoldHeight = 2f;

    [Header("Ground Check")]
    public bool IsGrounded;
    [SerializeField] Transform _groundCheck;
    [SerializeField] LayerMask _groundLayer;
    [SerializeField] float _groundCheckRadius;

    [Header("StateMark")]
    public bool IsJumping;

    protected override void Awake()
    {
        base.Awake();

        // Initialize StateMachine
        IdleState = new Player_IdleState(this, _stateMachine, "Idle");
        MoveState = new Player_MoveState(this, _stateMachine, "Move");
        AirState = new Player_AirState(this, _stateMachine, "InAir");
        JumpState = new Player_JumpState(this, _stateMachine, "Jump");
        FallState = new Player_FallState(this, _stateMachine, "Fall");

        _stateMachine.InitState(IdleState);
    }
    protected override void Start()
    {
        base.Start();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    protected override void Update()
    {
        base.Update();

        GroundCheck();
    }

    void GroundCheck()
    {
        IsGrounded = !IsJumping &&
            Physics2D.OverlapCircle(
                _groundCheck.position,
                _groundCheckRadius,
                _groundLayer
            );
    }

    void OnDrawGizmos()
    {
        Gizmos.color = IsGrounded ? Color.green : Color.red;
        Gizmos.DrawWireSphere(_groundCheck.position, _groundCheckRadius);
    }
}