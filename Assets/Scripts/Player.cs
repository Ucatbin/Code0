using UnityEngine;

public class Player : Entity
{
    [Header("StateMachine")]
    public Player_IdleState IdleState { get; private set; }
    public Player_MoveState MoveState { get; private set; }
    public Player_AirState AirState { get; private set; }
    public Player_JumpState JumpState { get; private set; }
    public Player_FallState FallState { get; private set; }
    public Player_EdgeState EdgeState { get; private set; }
    public Player_HookedState HookedState { get; private set; }
    public Player_AirGlideState AirGlideState { get; private set; }

    [Header("NecessaryComponent")]
    public Rigidbody2D Rb;
    public PlayerChecker Checker;
    public PlayerInput InputSystem;
    public GrappingHook GrappingHook;

    [Header("JumpAttribute")]
    public float JumpWindow = 0.2f;
    public float JumpDelay = 0.06f;
    public float MinGravityTrashold = 8f;
    public float AirGlideThreshold = 4f;

    [Header("GravityScale")]
    public float DefaultGravity = 1f;
    public float JumpGravity = 3f;
    public float FallGravityMax = 4.5f;
    public float FallGravityMin = 1f;

    [Header("PlayerAttribute")]
    // Movement
    public float GroundMoveForce = 20f;
    public float AirMoveForce = 10f;
    public float GLineMoveForce = 5f;
    public float MaxGroundSpeed = 8f;
    public float MaxAirSpeed = 6f;
    [Space(5)]
    // Jump
    public float JumpHeight = 3f;
    public float JumpHoldForce = 2f;

    [Header("StateMark")]
    public bool IsJumping; // Can player add force after jump
    public bool IsAttached; // Is the grappling hook attached
    public bool IsAttacking; // Is the player attacking

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
        AirGlideState = new Player_AirGlideState(this, _stateMachine, "AirGlide");

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
        _stateMachine.ChangeState(AirState);
        // Start grapple cooldown
        Player_TimerManager.Instance.GHookTimer.StartTimer(GrappingHook.GrappleCD);
    }
}