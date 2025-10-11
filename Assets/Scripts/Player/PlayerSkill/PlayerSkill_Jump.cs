using UnityEngine;

public class PlayerSkill_Jump : PlayerSkill_BaseSkill
{
    public PlayerSkill_Jump(PlayerController_Main player) : base(player)
    {
    }

    void OnEnable()
    {
        InputEvents.OnJumpPressed += TryUseSkill;
        InputEvents.OnJumpReleased += () =>
        {
            IsInputReset = true;
            if (_player.IsJumping)
                SkillEvents.TriggerJumpEnd();
        };
    }
    void OnDisable()
    {
        InputEvents.OnJumpPressed -= TryUseSkill;
        InputEvents.OnJumpReleased -= () =>
        {
            IsInputReset = true;
            if (_player.IsJumping)
                SkillEvents.TriggerJumpEnd();
        };
    }

    void Update()
    {
        if (CurrentCharges != MaxCharges)
            TryResetSkill();
    }
    public override void TryUseSkill()
    {
        if (!_isReady ||
            !IsInputReset ||
            CurrentCharges == 0 ||
            _player.IsBusy
        )
            return;

        UseSkill();
    }
    public override void UseSkill()
    {
        if (_player.IsWallSliding)
            SkillEvents.TriggerWallJumpStart();
        else
            SkillEvents.TriggerJumpStart();
    }
    public override void CoolDownSkill(float coolDown, string tag)
    {

    }
    public override void TryResetSkill()
    {
        if ((_player.Checker.IsGrounded || _player.IsWallSliding) && IsInputReset)
            CurrentCharges = MaxCharges;
    }
}