using UnityEngine;

public class Player_AttackState : Player_BaseState
{
    PlayerSkill_Attack _attackSkill;

    public Player_AttackState(PlayerController_Main entity, StateMachine stateMachine, int priority, string stateName) : base(entity, stateMachine, priority, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _player.IsBusy = true;
        _attackSkill = Player_SkillManager.Instance.Attack;
        _attackSkill.CurrentCharges -= _attackSkill.MaxCharges == -1 ? 0 : 1;
        _attackSkill.IsReady = false;
        _attackSkill.CanUse = false;
        _attackSkill.StartCoroutine(_attackSkill.AttackAnim());

        TimerManager.Instance.AddTimer(
            _attackSkill.AttackDuration,
            () => { SkillEvents.TriggerAttackEnd(); },
            "Player_AbilityTimer"
        );

        _player.SetTargetSpeed(Vector2.zero);
        _player.Rb.gravityScale = _player.PropertySO.AttackGravity;
        _player.Rb.linearVelocityY = 0f;
        _player.SetTargetSpeed(_player.InputSys.MouseDir * _attackSkill.AttackForce);
    }
    public override void PhysicsUpdate()
    {

    }
    public override void LogicUpdate()
    {

    }
    public override void Exit()
    {
        base.Exit();

        _player.IsBusy = false;
        _attackSkill.CoolDownSkill(_attackSkill.SkillCD, "PlyaerAttack");
        _player.Rb.gravityScale = _player.PropertySO.FallGravity;
    }
}