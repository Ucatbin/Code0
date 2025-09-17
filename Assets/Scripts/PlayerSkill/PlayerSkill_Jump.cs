using UnityEngine;

public class PlayerSkill_Jump : PlayerSkill_BaseSkill
{
    public bool FinishJump;

    public PlayerSkill_Jump(PlayerController_Main player) : base(player)
    {
    }

    void Update()
    {
        TryUseSkill();
    }

    public override void TryUseSkill()
    {
        if (!CanUseSkill ||
            CurrentCharges == 0 ||
            !_inputSys.JumpTrigger
        )
            return;
        UseSkill();
    }
    public override void UseSkill()
    {
        CurrentCharges -= MaxCharges == -1 ? 0 : 1;
        CanUseSkill = false;

        SkillEvents.TriggerJumpStart();
    }
    public override void CoolDownSkill(float coolDown, string tag)
    {
        throw new System.NotImplementedException();
    }
    public override void ResetSkill()
    {
        CurrentCharges = MaxCharges;
        CanUseSkill = true;
    }
}
