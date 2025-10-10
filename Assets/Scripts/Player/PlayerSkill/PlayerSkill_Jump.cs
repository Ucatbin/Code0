using System.Collections;

public class PlayerSkill_Jump : PlayerSkill_BaseSkill
{
    public PlayerSkill_Jump(PlayerController_Main player) : base(player)
    {
    }

    void Update()
    {
        TryUseSkill();
    }

    public override void TryUseSkill()
    {
        // Reset charges when on ground or wall slide
        if ((_player.Checker.IsGrounded || _player.IsWallSliding) && !_player.InputSys.JumpTrigger)
            CurrentCharges = MaxCharges;
            
        if (!IsInputReset ||
            CurrentCharges == 0 ||
            !_inputSys.JumpTrigger ||
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
    public override void ResetSkill()
    {
        IsReady = true;
        StartCoroutine(ButtonReleaseCheck());
    }

    public override IEnumerator ButtonReleaseCheck()
    {
        while (!IsInputReset)
        {
            if (!_player.InputSys.JumpTrigger)
                IsInputReset = true;
            else
                yield return null;
        }
    }
}