using UnityEngine;

public class PlayerController : EntityContoller
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
    [Header("SO")]
    public PlayerAttributeSO PlayerSO;

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
        IdleState = new Player_IdleState(this, _stateMachine, 0, "Idle");
        MoveState = new Player_MoveState(this, _stateMachine, 1, "Move");
        AirState = new Player_AirState(this, _stateMachine, 0, "InAir");
        JumpState = new Player_JumpState(this, _stateMachine, 2, "Jump");
        FallState = new Player_FallState(this, _stateMachine, 0, "Fall");
        HookedState = new Player_HookedState(this, _stateMachine, 3,"Hooked");
        AirGlideState = new Player_AirGlideState(this, _stateMachine, 1, "AirGlide");

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
            _stateMachine.ChangeState(HookedState, true);
            IsAttached = true;
        }
    }
    void HandleHookReleased()
    {
        _stateMachine.ChangeState(AirState, true);
        Player_SkillManager.Instance.GrappingHook.CoolDownSkill();
    }
    #endregion
}