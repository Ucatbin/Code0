using System.Collections;
using UnityEngine;

public class PlayerSkill_GrappingHookDash : PlayerSkill_BaseSkill
{
    [Header("GHookAttribute")]
    [SerializeField] float _lineDashSpeed = 45f; // Maximum distance to detect grapple points
    [SerializeField] float _lineDashForce = 5f;
    [SerializeField] float _duration = 0.25f;

    public PlayerSkill_GrappingHookDash(PlayerController_Main player) : base(player) { }

    public override void TryUseSkill()
    {
        // TODO:Havent complete if yet
        if (!_isReady ||
            !IsInputReset ||
            CurrentCharges == 0 ||
            !_player.IsHooked ||
            !_inputSys.DashTrigger
        )
            return;
        UseSkill();
    }
    public override void UseSkill()
    {
        StartCoroutine(LineDash());
    }
    public override void CoolDownSkill(float coolDown, string tag)
    {
        TimerManager.Instance.AddTimer(
            coolDown,
            () => { CoolDownSkill(); },
            tag
        );
    }
    public override void TryResetSkill()
    {

    }
    
    Vector2 CalculateForce(Vector2 lineDir, Vector2 normal)
    {
        Vector2 dir = new Vector2(-normal.y, normal.x);
        float dotProduct = Vector2.Dot(lineDir, dir);
        Vector2 dashDir = dotProduct > 0 ? dir : -dir;

        return dashDir.normalized * _lineDashForce;
    }
    public IEnumerator LineDash()
    {
        PlayerSkill_GrappingHook grappingHook = Player_SkillManager.Instance.GrappingHook;
        Vector2 lineDir = grappingHook.HookPoint.transform.position - _player.transform.position;
        float dashSpeed = _lineDashSpeed;
        float elapsedTime = 0f;
        _player.IsLineDashing = true;
        ConsumeSkill();
        while (dashSpeed != 1f && _player.IsHooked)
        {
            float t = elapsedTime / _duration;
            dashSpeed = Mathf.Lerp(dashSpeed, 1f, t);
            grappingHook.RopeJoint.distance -= dashSpeed * Time.fixedDeltaTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _player.IsLineDashing = false;
        grappingHook.ReleaseGHook();
        _player.Rb.linearVelocityY = 0f;
        _player.SetTargetVelocity(_player.Rb.linearVelocity + CalculateForce(lineDir, grappingHook.SurfaceNormal));
        _player.Rb.AddForce(_player.PropertySO.JumpInitPower * Vector2.up, ForceMode2D.Impulse);
        CoolDownSkill(SkillCD, "PlayerSkill");
    }
}