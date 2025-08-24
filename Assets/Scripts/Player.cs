using UnityEngine;

public class Player : Entity
{
    [Header("State Machine")]
    public Player_IdleState IdleState { get; private set; }
    public Player_MoveState MoveState { get; private set; }
    public Player_AirState AirState { get; private set; }
    public Player_JumpState JumpState { get; private set; }
    public Player_FallState FallState { get; private set; }
    public Player_EdgeState EdgeState { get; private set; }
    public Player_HookedState HookedState { get; private set; }

    [Header("Player Component")]
    public Rigidbody2D Rb;
    public PlayerChecker Checker;
    public PlayerInput InputSystem;

    [Header("InputSystem")]
    public float JumpWindow = 0.2f;
    public float JumpDelay = 0.06f;
    public float JumpGravity = 3f;
    public float FallGravity = 5f;

    [Header("Player Attribute")]
    public float MoveSpeed = 1f;
    public float MoveSpeedAir = 0.8f;
    public float JumpHeight = 3f;
    public float JumpHoldHeight = 2f;

    [Header("StateMark")]
    public bool IsJumping;
    public bool IsHoldingEdge;
    public bool IsAttacking;

    void OnEnable()
    {
        GrappleEvent.OnHookAttached += HandleHookAtteched;
        GrappleEvent.OnHookReleased += HandleHookReleased;
    }
    void OnDisable()
    {
        GrappleEvent.OnHookAttached -= HandleHookAtteched;
        GrappleEvent.OnHookReleased -= HandleHookReleased;
    }

    protected override void Awake()
    {
        base.Awake();

        // Initialize StateMachine
        IdleState = new Player_IdleState(this, _stateMachine, "Idle");
        MoveState = new Player_MoveState(this, _stateMachine, "Move");
        AirState = new Player_AirState(this, _stateMachine, "InAir");
        JumpState = new Player_JumpState(this, _stateMachine, "Jump");
        FallState = new Player_FallState(this, _stateMachine, "Fall");
        EdgeState = new Player_EdgeState(this, _stateMachine, "Edged");
        HookedState = new Player_HookedState(this, _stateMachine, "Hooked");

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
    }

    void HandleHookAtteched()
    {
        if (!IsAttacking)
            _stateMachine.ChangeState(HookedState);
    }
    void HandleHookReleased()
    {
        _stateMachine.ChangeState(IdleState);
    }
}