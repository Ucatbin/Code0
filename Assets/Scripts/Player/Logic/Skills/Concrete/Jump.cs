using UnityEngine;

public class PlayerSkill_Jump : PlayerSkill_BaseSkill
{
    [Header("Jump")]
    public float JumpInitPower = 14f;
    public float JumpInputWindow = 0.25f;
    public float JumpHoldSpeed = 10f;
    public float CoyoteWindow = 0.15f;
    [Header("WallJump")]
    public float WallJumpPower = 14f;
    public float WallJumpWindow = 0.15f;
    public Vector2 WallJumpDir = new Vector2(0.8f, 1f);

    public PlayerSkill_Jump(PlayerController_Main player) : base(player)
    {
    }

    void OnEnable()
    {
        InputEvents.OnJumpPressed += TryUseSkill;
        InputEvents.OnJumpReleased += () =>
        {
            if (_player.IsJumping)
                SkillEvents.TriggerJumpEnd();
            IsInputReset = true;
        };
    }
    void OnDisable()
    {
        InputEvents.OnJumpPressed -= TryUseSkill;
        InputEvents.OnJumpReleased -= () =>
        {
            if (_player.IsJumping)
                SkillEvents.TriggerJumpEnd();
            IsInputReset = true;
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
        if ((_player.CheckerSys.IsGrounded || _player.IsWallSliding) && IsInputReset)
            CurrentCharges = MaxCharges;
    }
}