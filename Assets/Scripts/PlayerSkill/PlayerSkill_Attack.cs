using UnityEngine;

public class PlayerSkill_Attack : PlayerSkill_BaseSkill
{
    public float AttackDuration = 0.2f;
    public float AttackForce = 2.5f;
    public float ForceDamping = 0.25f;
    [SerializeField] Animator _anim;
    [SerializeField] TrailRenderer _trail;

    public PlayerSkill_Attack(PlayerController player) : base(player)
    {
    }

    void Update()
    {
        CheckLineDash();
    }
    public override void CheckLineDash()
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

        _trail.enabled = true;
        _trail.time = AttackDuration * 2.5f;
        _anim.SetBool("Attack", true);
    }
    public override void CoolDownSkill()
    {
        Player_TimerManager.Instance.AddTimer(
            CoolDown,
            () => { ResetSkill(); },
            "Player_AbilityTimer"
        );

        _trail.enabled = false;
        _anim.SetBool("Attack", false);
    }

    public override void ResetSkill()
    {
        CanUseSkill = true;
    }

}
