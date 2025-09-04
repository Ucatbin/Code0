using UnityEngine;

public class PlayerSkill_GrappingHook : PlayerSkill_BaseSkill
{
    [SerializeField] float _breakCoolDown = 2f;
    [Header("NecessaryComponent")]
    [field:SerializeField] public DistanceJoint2D RopeJoint { get; private set; }
    [field:SerializeField] public LineRenderer RopeLine { get; private set; }
    public GameObject HookPoint { get; private set; } // The point where the hook is attached
    [SerializeField] HookPointPool _pool;

    [Header("GHookAttribute")]
    [SerializeField] float _lineMoveSpeed = 4.5f;
    public float LineSwingForce = 10f;
    public float MaxSwingSpeed = 10f;
    [field: SerializeField] public float MaxDetectDist { get; private set; } = 20f; // Maximum distance to detect grapple points
    [field:SerializeField] public LayerMask CanHookLayer { get; private set; } // Which layer can the hook attach to
    RaycastHit2D _hit;

    public PlayerSkill_GrappingHook(PlayerController player) : base(player) { }

    void Update()
    {
        BasicCheck();
    }
    public override void BasicCheck()
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
        Vector2 mousePos = _player.MainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 fireDir = (mousePos - (Vector2)_player.transform.position).normalized;

        _hit = Physics2D.Raycast(
            _player.transform.position,
            fireDir,
            MaxDetectDist,
            CanHookLayer
        );

        if (_hit.collider != null)
        {
            HookPoint = _pool.Pool.Get();
            HookPoint.transform.position = _hit.point;
            HookPoint.transform.parent = _hit.transform;
            AttachHook();
        }
        else
            ResetSkill();
    }
    public override void CoolDownSkill()
    {
        Player_TimerManager.Instance.AddTimer(
            CoolDown,
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
        // Let state machine know the player is attached
        SkillEvents.TriggerHookAttached();
        // Set connect point and enable distance joint
        RopeJoint.connectedBody = HookPoint.GetComponent<Rigidbody2D>();
        RopeJoint.distance = Vector2.Distance(_player.transform.position, HookPoint.transform.position);
        RopeJoint.enabled = true;

        // Setup line renderer
        RopeLine.SetPosition(0, _player.transform.position);
        RopeLine.SetPosition(1, HookPoint.transform.position);
        RopeLine.enabled = true;
    }

    public void ReleaseHook()
    {
        // Let state machine know the player is released
        SkillEvents.TriggerHookReleased();
        _player.IsAttached = false;

        // Disable distance joint and line renderer
        RopeJoint.enabled = false;
        RopeLine.enabled = false;
        _pool.Pool.Release(HookPoint);
    }
    public void BreakHook()
    {
        
    }

    public void MoveOnGLine()
    {
        float inputY = _player.InputSys.MoveInput.y;
        if (inputY != 0)
            RopeJoint.distance -= _lineMoveSpeed * inputY * Time.fixedDeltaTime;
    }

}