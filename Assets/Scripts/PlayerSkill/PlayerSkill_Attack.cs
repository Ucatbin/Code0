using UnityEngine;

public class PlayerSkill_Attack : PlayerSkill_BaseSkill
{
    public float AttackDuration = 0.2f;
    public float AttackMovement = 3f;
    public PlayerSkill_Attack(PlayerController player) : base(player)
    {
    }

    void Update()
    {
        BasicCheck();
    }
    public override void BasicCheck()
    {
        // Check input and CanUseSkill
        if (!_inputSys.AttackTrigger || !CanUseSkill)
            return;
        UseSkill();
    }
    public override void UseSkill()
    {
        if (_player.IsAttached)
            return;
        CanUseSkill = false;
        SkillEvents.TriggerAttack();
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
