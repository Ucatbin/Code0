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

        TimerManager.Instance.AddTimer(
            _attackSkill.AttackDuration,
            () => { SkillEvents.TriggerAttackEnd(); },
            "Player_AbilityTimer"
        );

        _player.RTProperty.TargetSpeed = Vector2.zero;
        _player.Rb.gravityScale = _player.PropertySO.AttackGravity;

        _player.RTProperty.TargetSpeed = _player.InputSys.MouseDir *
            _attackSkill.AttackForce;
    }
    public override void PhysicsUpdate()
    {
        _player.Rb.linearVelocity = _player.RTProperty.TargetSpeed;
    }
    public override void LogicUpdate()
    {

    }
    public override void Exit()
    {
        base.Exit();

        _player.RTProperty.TargetSpeed = Vector2.zero;
        _attackSkill.CoolDownSkill(_attackSkill.SkillCD, "PlyaerAttack");
        _player.Rb.gravityScale = _player.PropertySO.FallGravity;
    }
}