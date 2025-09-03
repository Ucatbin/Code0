using UnityEngine;

public class PlayerSkill_Attack : PlayerSkill_BaseSkill
{
    public float AttackDuration = 0.2f;
    public PlayerSkill_Attack(PlayerController player) : base(player)
    {
    }

    public override void UseSkill()
    {
        CanUseSkill = false;
    }
    public override void CoolDownSkill()
    {
        Player_TimerManager.Instance.AddTimer(
            CoolDown,
            () => { ResetSkill(); },
            "Player_AbilityTimer"
        );
    }

    public override void ResetSkill()
    {
        CanUseSkill = true;
    }

}
