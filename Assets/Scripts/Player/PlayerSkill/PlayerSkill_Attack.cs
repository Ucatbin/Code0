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
    public LayerMask CanHit;

    public PlayerSkill_Attack(PlayerController_Main player) : base(player)
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
        CanUse = false;

        SkillEvents.TriggerAttackStart();
        _anim.speed = 1 / AttackDuration;
        float angleZ = Vector2.SignedAngle(Vector2.right, _player.InputSys.MouseDir);
        if ((Mathf.Abs(angleZ) > 90 && _player.FacingDir != -1) ||
            (Mathf.Abs(angleZ) < 90 && _player.FacingDir != 1)
        )
            {
                _player.Root.Rotate(new Vector2(0f, 180f));
                _player.FacingDir = _player.FacingDir * -1;
            }

        _attackItem.rotation = Quaternion.Euler(0, 0, angleZ);
        _attackItem.gameObject.SetActive(true);
        _anim.SetBool("Attack", true);
    }
    public override void CoolDownSkill(float coolDown, string tag)
    {
        TimerManager.Instance.AddTimer(
            coolDown,
            () => { ResetSkill(); },
            tag
        );

        _attackItem.gameObject.SetActive(false);
        _anim.SetBool("Attack", false);
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
            if (!_player.InputSys.AttackTrigger)
                CanUse = true;
            else
                yield return null;
        }
    }
}