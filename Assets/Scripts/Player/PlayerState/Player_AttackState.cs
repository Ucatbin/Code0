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

        _attackSkill = Player_SkillManager.Instance.Attack;

        _attackSkill.StartCoroutine(_attackSkill.AttackAnim());
        _player.SetTargetVelocity(_player.Rb.linearVelocity + _player.InputSys.MouseDir * _attackSkill.AttackForce);
        _player.ApplyMovement();

        TimerManager.Instance.AddTimer(
            _attackSkill.AttackDuration,
            () => { SkillEvents.TriggerAttackEnd(); },
            "Player_AbilityTimer"
        );

        _attackSkill.ConsumeSkill();
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
        _player.IsAttacking = false;

        _attackSkill.CoolDownSkill(_attackSkill.SkillCD, "PlyaerAttack");
    }
}