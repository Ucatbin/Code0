using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSkill_GrappingHookDash : PlayerSkill_BaseSkill
{
    PlayerSkill_GrappingHook _gHookSkill;
    [Header("GHookAttribute")]
    [SerializeField] float _lineDashForce = 5f;
    [SerializeField] bool _stopDash = false;

    public PlayerSkill_GrappingHookDash(PlayerController_Main player) : base(player) { }

    void OnEnable()
    {
        InputEvents.OnLineDashPressed += TryUseSkill;
        InputEvents.OnLineDashReleased += () =>
        {
            IsInputReset = true;
            _stopDash = true;
        };
    }
    void OnDisable()
    {
        InputEvents.OnLineDashPressed -= TryUseSkill;
        InputEvents.OnLineDashReleased -= () =>
        {
            IsInputReset = true;
            _stopDash = true;
        };
    }
    public override void TryUseSkill()
    {
        _stopDash = false;
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
        CoolDownSkill(SkillCD, "PlayerSkill");
        StartCoroutine(ApplyLineDash());
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

    IEnumerator ApplyLineDash()
    {
        while (!_stopDash && _player.IsHooked)
        {
            Vector2 playerToHook = (_gHookSkill.HookPoint.transform.position - _player.transform.position).normalized;
            Vector2 tangent1 = new Vector2(-playerToHook.y, playerToHook.x);
            Vector2 tangent2 = new Vector2(playerToHook.y, -playerToHook.x);
            Vector2 dashDir = _player.FacingDir >= 0 ? tangent2 : tangent1;

            _player.Rb.AddForce(_lineDashForce * dashDir.normalized, ForceMode2D.Force);
            Debug.Log( dashDir.normalized * playerToHook);
            yield return new WaitForFixedUpdate();
        }
        yield break;
    }
}