using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkill_GrappingHookDash : PlayerSkill_BaseSkill
{
    PlayerSkill_GrappingHook _gHookSkill;
    [Header("GHookAttribute")]
    [SerializeField] float _lineDashForce = 5f;
    [SerializeField] bool _stopDash = false;
    [SerializeField] float _gasConsumSpeed;
    [SerializeField] float _currentGas;

    [Header("Component")]
    [SerializeField] Slider _gasDisplay;

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
        _gHookSkill = Player_SkillManager.Instance.GrappingHook;
        // TODO:Havent complete if yet
        if (!_isReady ||
            !IsInputReset ||
            CurrentCharges == 0 ||
            !_player.IsHooked
        )
            return;
        UseSkill();
    }
    public override void UseSkill()
    {
        _stopDash = false;
        _currentGas = 1;
        _gasDisplay.value = _currentGas;
        // ConsumeSkill();
        // CoolDownSkill(SkillCD, "PlayerSkill");
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
        while (!_stopDash && _player.IsHooked && _currentGas > 0)
        {
            Vector2 playerToHook = (_gHookSkill.HookPoint.transform.position - _player.transform.position).normalized;
            Vector2 tangent1 = new Vector2(-playerToHook.y, playerToHook.x);
            Vector2 tangent2 = new Vector2(playerToHook.y, -playerToHook.x);
            Vector2 dashDir = _player.FacingDir >= 0 ? tangent2 : tangent1;

            _player.Rb.AddForce(_lineDashForce * dashDir.normalized, ForceMode2D.Force);
            _currentGas -= _gasConsumSpeed;
            _gasDisplay.value = _currentGas;
            yield return new WaitForFixedUpdate();
        }
        yield break;
    }
}