using System.Collections;
using UnityEngine;

public class PlayerSkill_GrappingHook : PlayerSkill_BaseSkill
{
    [SerializeField] float BreakCoolDown = 2f;
    [Header("NecessaryComponent")]
    [field: SerializeField] public DistanceJoint2D RopeJoint { get; private set; }
    [field: SerializeField] public LineRenderer RopeLine { get; private set; }
    public GameObject HookPoint { get; private set; }   // The point where the hook is attached
    public Vector2 SurfaceNormal { get; private set; }     // The normal of the surface hit    
    [SerializeField] HookPointPool _pool;

    [Header("GHookAttribute")]
    [field: SerializeField] public float MaxDetectDist { get; private set; } = 15f; // Maximum distance to detect grapple points
    [field: SerializeField] public float MaxLineDist { get; private set; }
    [field: SerializeField] public LayerMask CanHookLayer { get; private set; }      // Which layer can the hook attach to
    [SerializeField] float _lineMoveSpeed = 4.5f;
    [SerializeField] float _lineSwingForce = 10f;
    [SerializeField] float _maxSwingSpeed = 10f;
    [SerializeField] float _initForce;
    [SerializeField] float _initSpeed;

    public PlayerSkill_GrappingHook(PlayerController_Main player) : base(player) { }

    void Update()
    {
        TryUseSkill();
    }

    public override void TryUseSkill()
    {
        if (!CanUseSkill ||
            CurrentCharges == 0 ||
            !_inputSys.GrapperTrigger ||
            _player.IsAttacking
        )
            return;
        UseSkill();
    }
    public override void UseSkill()
    {
        CurrentCharges -= MaxCharges == -1 ? 0 : 1;
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
    public override void CoolDownSkill(float coolDown, string tag)
    {
        TimerManager.Instance.AddTimer(
            coolDown,
            () => { ResetSkill(); },
            tag
        );
    }
    public override void ResetSkill()
    {
        CanUseSkill = true;
    }
    void AttachHook()
    {
        _player.IsAddingForce = true;
        _player.Rb.gravityScale = 0f;
        SetLineRenderer();
        if (_player.Checker.IsGrounded)
        {
            Vector3 targetPos = new Vector2(HookPoint.transform.position.x, _player.transform.position.y);
            _player.Rb.AddForce((targetPos - _player.transform.position) * _initForce, ForceMode2D.Impulse);
            StartCoroutine(MoveToTarget(_player.transform.position, targetPos));
        }
        else
        {
            if (Vector2.Distance(_player.transform.position, HookPoint.transform.position) > MaxLineDist)
                StartCoroutine(InitGLine());
            else
            {
                SetJoint();
                SetLineRenderer();
                SkillEvents.TriggerHookAttach();
            }
        }
    }

    IEnumerator MoveToTarget(Vector2 startPos, Vector2 targetPos)
    {
        float elapsedTime = 0f;
        while (elapsedTime < _initForce)
        {
            float t = elapsedTime / _initForce;
            _player.PlayerRoot.position = Vector2.Lerp(startPos, targetPos, t);
            SetLineRenderer();
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _player.PlayerRoot.position = targetPos;
        _player.Rb.AddForce((targetPos - startPos) * 1f, ForceMode2D.Impulse);

        if (Vector2.Distance(_player.transform.position, HookPoint.transform.position) > MaxLineDist)
            StartCoroutine(InitGLine());
        else
        {
            SetJoint();
            SkillEvents.TriggerHookAttach();
        }
    }
    IEnumerator InitGLine()
    {
        SetJoint();
        float elapsedTime = 0f;
        float startDist = RopeJoint.distance;
        while (elapsedTime <= _initSpeed)
        {
            float t = elapsedTime / _initSpeed;
            SetLineRenderer();
            RopeJoint.distance = Mathf.Lerp(startDist, MaxLineDist, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        RopeJoint.distance = MaxLineDist;
        SkillEvents.TriggerHookAttach();
    }
    public void ReleaseGHook()
    {
        HandleDisable();
        CoolDownSkill(SkillCD, "PlayerSkill");
    }
    public void BreakGHook()
    {
        HandleDisable();
        CoolDownSkill(BreakCoolDown, "PlayerSkill");
    }
    void HandleDisable()
    {
        // Let state machine know the player is released
        SkillEvents.TriggerHookRelease();
        _player.IsAttached = false;

        // Disable distance joint and line renderer
        RopeJoint.enabled = false;
        RopeLine.enabled = false;
        _pool.Pool.Release(HookPoint);
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