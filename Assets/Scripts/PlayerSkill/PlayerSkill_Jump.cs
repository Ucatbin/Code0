using UnityEngine;

public class PlayerSkill_Jump : PlayerSkill_BaseSkill
{
    public bool FinishJump;

    public PlayerSkill_Jump(PlayerController player) : base(player)
    {
    }

    void Update()
    {
        TryUseSkill();
    }

    public override void TryUseSkill()
    {
        if (!CanUseSkill ||
            (MaxCharges != -1 && CurrentCharges == 0) ||
            !_inputSys.JumpTrigger
        )
            return;
        UseSkill();
    }
    public override void UseSkill()
    {
        CurrentCharges -= MaxCharges != -1 ? 1 : 0;
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
