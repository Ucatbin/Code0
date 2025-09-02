using UnityEngine;

public class Player : Entity
{
    [Header("StateMachine")]
    public Player_IdleState IdleState { get; private set; }
    public Player_MoveState MoveState { get; private set; }
    public Player_AirState AirState { get; private set; }
    public Player_JumpState JumpState { get; private set; }
    public Player_FallState FallState { get; private set; }
    public Player_HookedState HookedState { get; private set; }
    public Player_AirGlideState AirGlideState { get; private set; }

    [Header("NecessaryComponent")]
    public Rigidbody2D Rb;
    public PlayerChecker Checker;
    public PlayerInput InputSys;

    [Header("JumpAttribute")]
    public float JumpWindow = 0.2f;
    public float JumpDelay = 0.06f;
    public float MinGravityTrashold = 8f;
    public float AirGlideThreshold = 4f;

    [Header("GravityScale")]
    [SerializeField] float _defaultGravity = 1f;
    [SerializeField] float _jumpGravity = 3f;
    [SerializeField] float _fallGravityMax = 4.5f;
    [SerializeField] float _fallGravityMin = 1f;
    
    public float DefaultGravity => _defaultGravity;
    public float JumpGravity => _jumpGravity;
    public float FallGravityMax => _fallGravityMax;
    public float FallGravityMin => _fallGravityMin;

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
    public bool IsJumping;      // Can player add force after jump
    public bool IsAttached;     // Is the grappling hook attached
    public bool IsAttacking;    // Is the player attacking

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

    #region GrappingHook logics
    void HandleHookAtteched()
    {
        // TODO: More check conditions
        if (!IsAttacking)
        {
            _stateMachine.ChangeState(HookedState);
            IsAttached = true;
        }
    }
    void HandleHookReleased()
    {
        _stateMachine.ChangeState(AirState);
        Player_SkillManager.Instance.GrappingHook.CoolDownSkill();
    }
    #endregion
}