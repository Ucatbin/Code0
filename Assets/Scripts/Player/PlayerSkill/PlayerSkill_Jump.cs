using System.Collections;
using UnityEngine;

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
        if (!CanUse ||
            CurrentCharges == 0 ||
            !_inputSys.JumpTrigger
        )
            return;
        UseSkill();
    }
    public override void UseSkill()
    {
        CurrentCharges -= MaxCharges == -1 ? 0 : 1;
        CanUse = false;
        IsReady = false;

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
        while (!CanUse)
        {
            if (!_player.InputSys.JumpTrigger)
                    CanUse = true;
            else
                yield return null;
        }
    }
}
