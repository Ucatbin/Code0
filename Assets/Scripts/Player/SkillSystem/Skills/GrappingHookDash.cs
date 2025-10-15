using System.Collections;
using UnityEngine;

public class PlayerSkill_GrappingHookDash : PlayerSkill_BaseSkill
{
    PlayerSkill_GrappingHook _gHookSkill;
    [Header("GHookAttribute")]
    [SerializeField] float _lineDashForce = 5f;

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
        ApplyLineDash();
        CoolDownSkill(SkillCD, "PlayerSkill");
        // StartCoroutine(LineDash());
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

    void ApplyLineDash()
    {
        Vector2 tangent1 = new Vector2(-_gHookSkill.SurfaceNormal.y, _gHookSkill.SurfaceNormal.x);
        Vector2 tangent2 = new Vector2(_gHookSkill.SurfaceNormal.y, -_gHookSkill.SurfaceNormal.x);
        Vector2 dashDir = _player.FacingDir == 1 ? tangent1 : tangent2;

        _player.Rb.AddForce(_lineDashForce * dashDir.normalized, ForceMode2D.Impulse);
    }
}