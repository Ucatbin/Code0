using UnityEngine;

public class PlayerSkill_GrappingHook : Player_BaseSkill
{
    [Header("NecessaryComponent")]
    [SerializeField] Camera _mainCam;
    [field:SerializeField] public DistanceJoint2D RopeJoint { get; private set; }
    [field:SerializeField] public LineRenderer RopeLine { get; private set; }
    public GameObject HookPoint { get; private set; } // The point where the hook is attached
    [SerializeField] HookPointPool _pool;

    [Header("GHookAttribute")]
    [SerializeField] float _lineMoveSpeed = 4.5f;
    [field:SerializeField] public float MaxDetectDist { get; private set; } = 20f; // Maximum distance to detect grapple points
    [field:SerializeField] public LayerMask CanHookLayer { get; private set; } // Which layer can the hook attach to
    RaycastHit2D _hit;

    public PlayerSkill_GrappingHook(Player player) : base(player) { }

    void Update()
    {
        // Do nothing when GrapperTrigger not pressed
        if (!_inputSys.GrapperTrigger || _player.IsAttached)
            return;

            UseSkill();
    }
    public override void UseSkill()
    {
        if (!CanUseSkill)
            return;
        // Get mouse position and calculate fire direction
        Vector2 mousePos = _mainCam.ScreenToWorldPoint(Input.mousePosition);
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
        GrappleEvent.TriggerHookAttached();
        CanUseSkill = false;
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
        GrappleEvent.TriggerHookReleased();
        _player.IsAttached = false;

        // Disable distance joint and line renderer
        RopeJoint.enabled = false;
        RopeLine.enabled = false;
        _pool.Pool.Release(HookPoint);
    }

    public void MoveOnGLine()
    {
        float inputY = _player.InputSys.MoveInput.y;
        if (inputY != 0)
            RopeJoint.distance -= _lineMoveSpeed * inputY * Time.fixedDeltaTime;
    }
}