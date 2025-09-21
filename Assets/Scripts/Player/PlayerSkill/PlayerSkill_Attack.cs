using UnityEngine;

public class PlayerSkill_Attack : PlayerSkill_BaseSkill
{
    [Tooltip("Duration of single attack")]
    public float AttackDuration = 0.2f;
    [Tooltip("Offset while attacking")]
    public float AttackForce = 2.5f;
    [Tooltip("The animator of attack effect")]
    [SerializeField] Animator _anim;
    [Tooltip("The parent of attack effect")]
    [SerializeField] Transform _animation;

    public PlayerSkill_Attack(PlayerController_Main player) : base(player)
    {
    }

    void Update()
    {
        TryUseSkill();
    }

    public override void TryUseSkill()
    {
        if (!IsReady ||
            CurrentCharges == 0 ||
            !_inputSys.AttackTrigger ||
            _player.IsAttached
        )
            return;
        UseSkill();
    }
    public override void UseSkill()
    {
        BuffItem buff_speedUp = BuffFactory.CreateBuffItem(
            BuffManager.Instance.Buff_SpeedUp,
            _player.gameObject,
            _player.gameObject,
            1
        );
        
        _player.BuffHandler.AddBuff(buff_speedUp);
        CurrentCharges -= MaxCharges == -1 ? 0 : 1;
        IsReady = false;

        SkillEvents.TriggerAttackStart();
        _anim.speed = 1 / AttackDuration;
        float angleZ = Vector2.SignedAngle(Vector2.right, _player.InputSys.MouseDir);
        _animation.rotation = Quaternion.Euler(0, 0, angleZ);
        _animation.gameObject.SetActive(true);
        _anim.SetBool("Attack", true);
    }
    public override void CoolDownSkill(float coolDown, string tag)
    {
        TimerManager.Instance.AddTimer(
            coolDown,
            () => { ResetSkill(); },
            tag
        );

        _animation.gameObject.SetActive(false);
        _anim.SetBool("Attack", false);
    }
    public override void ResetSkill()
    {
        IsReady = true;
    }
}