using UnityEngine;

public class PlayerSkill_Attack : PlayerSkill_BaseSkill
{
    public float AttackDuration = 0.2f;
    public float AttackForce = 2.5f;
    [SerializeField] Animator _anim;
    [SerializeField] Transform _animation;

    public PlayerSkill_Attack(PlayerController player) : base(player)
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
            !_inputSys.AttackTrigger ||
            _player.IsAttached
        )
            return;
        UseSkill();
    }
    public override void UseSkill()
    {
        CurrentCharges -= MaxCharges != -1 ? 1 : 0;
        CanUseSkill = false;

        SkillEvents.TriggerAttackStart();

        float angleZ = Vector2.SignedAngle(Vector2.right, _player.InputSys.MouseDir);
        _animation.rotation = Quaternion.Euler(0, 0, angleZ);
        _animation.gameObject.SetActive(true);
        _anim.SetBool("Attack", true);
    }
    public override void CoolDownSkill(float coolDown, string tag)
    {
        Player_TimerManager.Instance.AddTimer(
            coolDown,
            () => { ResetSkill(); },
            tag
        );

        _animation.gameObject.SetActive(false);
        _anim.SetBool("Attack", false);
    }
    public override void ResetSkill()
    {
        CanUseSkill = true;
    }
}