using System.Collections;
using UnityEngine;

public class PlayerSkill_Attack : PlayerSkill_BaseSkill
{
    [SerializeField] public DamageData Damage;
    [Tooltip("Duration of single attack")]
    public float AttackDuration = 0.28f;
    [Tooltip("Offset while attacking")]
    public float AttackForce = 3.5f;
    [Tooltip("The animator of attack effect")]
    [SerializeField] Animator _anim;
    [Tooltip("The parent of attack effect")]
    [SerializeField] Transform _attackItem;

    public PlayerSkill_Attack(PlayerController_Main player) : base(player)
    {
    }

    void OnEnable()
    {
        InputEvents.OnAttackPressed += TryUseSkill;
        InputEvents.OnAttackReleased += () => IsInputReset = true;
    }
    void OnDisable()
    {
        InputEvents.OnAttackPressed -= TryUseSkill;
        InputEvents.OnAttackReleased -= () => IsInputReset = true;
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
        SkillEvents.TriggerAttackStart();
    }
    public override void CoolDownSkill(float coolDown, string tag)
    {
        TimerManager.Instance.AddTimer(
            coolDown,
            () => { CoolDownSkill(); },
            "AttackCD"
        );

        _attackItem.gameObject.SetActive(false);
        _anim.SetBool("Attack", false);
    }
    public override void TryResetSkill()
    {
        
    }

    public IEnumerator AttackAnim()
    {
        _anim.speed = 1 / AttackDuration;
        float angleZ = Vector2.SignedAngle(Vector2.right, _player.InputSys.MouseDir);

        _attackItem.rotation = Quaternion.Euler(0, 0, angleZ);
        _attackItem.gameObject.SetActive(true);
        _anim.SetBool("Attack", true);

        yield return null;
    }
}