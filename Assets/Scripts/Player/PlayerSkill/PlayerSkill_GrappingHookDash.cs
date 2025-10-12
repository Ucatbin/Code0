using System.Collections;
using UnityEngine;

public class PlayerSkill_GrappingHookDash : PlayerSkill_BaseSkill
{
    PlayerSkill_GrappingHook _gHookSkill;
    [Header("GHookAttribute")]
    [SerializeField] float _lineDashSpeed = 45f; // Maximum distance to detect grapple points
    [SerializeField] float _lineDashForce = 5f;
    [SerializeField] float _duration = 0.25f;

    public PlayerSkill_GrappingHookDash(PlayerController_Main player) : base(player) { }

    void OnEnable()
    {
        InputEvents.OnLineDashPressed += TryUseSkill;
        InputEvents.OnLineDashReleased += () => IsInputReset = true;
    }
    void OnDisable()
    {
        InputEvents.OnLineDashPressed -= TryUseSkill;
        InputEvents.OnLineDashReleased -= () => IsInputReset = true;
    }
    public override void TryUseSkill()
    {
        _gHookSkill = Player_SkillManager.Instance.GrappingHook;
        // TODO:Havent complete if yet
        if (!_isReady ||
            !IsInputReset ||
            CurrentCharges == 0 ||
            !_gHookSkill.IsHookFinished
        )
            return;
        UseSkill();
    }
    public override void UseSkill()
    {
        ConsumeSkill();
        StartCoroutine(LineDash());
    }
    public override void CoolDownSkill(float coolDown, string tag)
    {
        TimerManager.Instance.AddTimer(
            coolDown,
            () => { CoolDownSkill(); }
        );
    }
    public override void TryResetSkill()
    {

    }

    public IEnumerator LineDash()
    {
        Vector2 lineDir = _gHookSkill.HookPoint.transform.position - _player.transform.position;
        float dashSpeed = _lineDashSpeed;
        float elapsedTime = 0f;
        _player.IsLineDashing = true;
        while (dashSpeed != 1f && _player.IsHooked)
        {
            float t = elapsedTime / _duration;
            dashSpeed = Mathf.Lerp(dashSpeed, 1f, t);
            _gHookSkill.RopeJoint.distance -= dashSpeed * Time.fixedDeltaTime;
            elapsedTime += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        _player.ApplyMovement();
        _player.IsLineDashing = false;
        _gHookSkill.ReleaseGHook();
        _player.Rb.AddForce(_player.Rb.linearVelocity + CalculateForce(lineDir, _gHookSkill.SurfaceNormal), ForceMode2D.Impulse);
        CoolDownSkill(SkillCD, "PlayerSkill");
    }
    Vector2 CalculateForce(Vector2 lineDir, Vector2 normal)
    {
        Vector2 dir = new Vector2(-normal.y, normal.x);
        float dotProduct = Vector2.Dot(lineDir, dir);
        Vector2 dashDir = dotProduct > 0 ? dir : -dir;

        return dashDir.normalized * _lineDashForce;
    }
}