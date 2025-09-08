using System.Collections;
using UnityEngine;

public class PlayerSkill_GrappingHook : PlayerSkill_BaseSkill
{
    [SerializeField] float _breakCoolDown = 2f;
    [Header("NecessaryComponent")]
    [field: SerializeField] public DistanceJoint2D RopeJoint { get; private set; }
    [field: SerializeField] public LineRenderer RopeLine { get; private set; }
    public GameObject HookPoint { get; private set; }   // The point where the hook is attached
    public Vector2 SurfaceNormal { get; private set; }     // The normal of the surface hit    
    [SerializeField] HookPointPool _pool;

    [Header("GHookAttribute")]
    [field: SerializeField] public float MaxDetectDist { get; private set; } = 20f; // Maximum distance to detect grapple points
    [field: SerializeField] public LayerMask CanHookLayer { get; private set; }      // Which layer can the hook attach to
    [SerializeField] float _lineMoveSpeed = 4.5f;
    [SerializeField] float _lineSwingForce = 10f;
    [SerializeField] float _maxSwingSpeed = 10f;

    bool _isBroken;

    public float LineGroundMoveForce = 0.22f;

    public PlayerSkill_GrappingHook(PlayerController player) : base(player) { }

    void Update()
    {
        BasicSkillCheck();
    }
    public override void BasicSkillCheck()
    {
        // Check input and CanUseSkill
        if (!_inputSys.GrapperTrigger || !CanUseSkill)
            return;
        UseSkill();
    }
    public override void UseSkill()
    {
        if (_player.IsAttacking)
            return;
        CanUseSkill = false;

        // Get mouse position and calculate fire direction
        RaycastHit2D hit = Physics2D.Raycast(
            _player.transform.position,
            _player.InputSys.MouseDir,
            MaxDetectDist,
            CanHookLayer
        );

        if (hit.collider != null)
        {
            HookPoint = _pool.Pool.Get();
            HookPoint.transform.position = hit.point;
            HookPoint.transform.parent = hit.transform;
            SurfaceNormal = hit.normal;
            AttachHook();
        }
        else
            ResetSkill();
    }
    public override void CoolDownSkill()
    {
        Player_TimerManager.Instance.AddTimer(
            _isBroken ? _breakCoolDown : CoolDown,
            () => { ResetSkill(); },
            "Player_AbilityTimer"
        );
    }
    public override void ResetSkill()
    {
        CanUseSkill = true;
    }
    void AttachHook()
    {
        float heightDiff = HookPoint.transform.position.y - _player.transform.position.y;
        _player.Rb.gravityScale = 0f;
        SetLineRenderer();
        SetJoint();
        if (_player.Checker.IsGrounded)
            RopeJoint.distance = heightDiff - 0.5f;
        SkillEvents.TriggerHookAttach();
    }
    public void ReleaseGHook()
    {
        // Let state machine know the player is released
        SkillEvents.TriggerHookReleas();
        _player.IsAttached = false;
        _isBroken = false;

        // Disable distance joint and line renderer
        RopeJoint.enabled = false;
        RopeLine.enabled = false;
        _pool.Pool.Release(HookPoint);
        CoolDownSkill();
    }
    public void BreakGHook()
    {
        // Let state machine know the player is released
        SkillEvents.TriggerHookReleas();
        _player.IsAttached = false;
        _isBroken = true;

        // Disable distance joint and line renderer
        RopeJoint.enabled = false;
        RopeLine.enabled = false;
        _pool.Pool.Release(HookPoint);
        CoolDownSkill();
    }
    public void MoveOnGLine()
    {
        float inputY = _player.InputSys.MoveInput.y;
        float inputX = _player.InputSys.MoveInput.x;
        if (inputY != 0)
            RopeJoint.distance -= _lineMoveSpeed * inputY * Time.fixedDeltaTime;
        if (inputX != 0 && Mathf.Abs(_player.Rb.linearVelocityX) <= _maxSwingSpeed)
            _player.Rb.AddForce(Vector2.right * inputX * _lineSwingForce);
    }
    void SetJoint()
    {
        RopeJoint.connectedBody = HookPoint.GetComponent<Rigidbody2D>();
        RopeJoint.distance = Vector2.Distance(_player.transform.position, HookPoint.transform.position);
        RopeJoint.enabled = true;
    }
    void SetLineRenderer()
    {
        RopeLine.SetPosition(0, _player.transform.position);
        RopeLine.SetPosition(1, HookPoint.transform.position);
        RopeLine.enabled = true;
    }
}