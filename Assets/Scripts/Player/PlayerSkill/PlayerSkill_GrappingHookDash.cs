using System.Collections;
using Unity.VisualScripting;
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
        if (!CanUse ||
            CurrentCharges == 0 ||
            !_inputSys.DashTrigger
        )
            return;
        UseSkill();
    }
    public override void UseSkill()
    {
        CurrentCharges -= MaxCharges == -1 ? 0 : 1;
        IsReady = false;
        CanUse = false;

        StartCoroutine(LineDash());
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
        IsReady = true;
        StartCoroutine(ButtonReleaseCheck());
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
        while (dashSpeed != 1f && _player.IsAttached)
        {
            float t = elapsedTime / _duration;
            dashSpeed = Mathf.Lerp(dashSpeed, 1f, t);
            grappingHook.RopeJoint.distance -= dashSpeed * Time.fixedDeltaTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        grappingHook.ReleaseGHook();
        _player.Rb.linearVelocityY = 0f;
        _player.RTProperty.TargetSpeed += CalculateForce(lineDir, grappingHook.SurfaceNormal);
        _player.Rb.AddForce(_player.PropertySO.JumpInitForce * Vector2.up, ForceMode2D.Impulse);
        CoolDownSkill(SkillCD, "PlayerSkill");
    }

    public override IEnumerator ButtonReleaseCheck()
    {
        while (!CanUse)
        {
            if (!_player.InputSys.DashTrigger)
                CanUse = true;
            else
                yield return null;
        }
    }
}