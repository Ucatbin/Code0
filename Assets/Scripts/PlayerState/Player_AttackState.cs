using UnityEngine;

public class Player_AttackState : Player_BaseState
{
    PlayerSkill_Attack _attackSkill;

    public Player_AttackState(PlayerController entity, StateMachine stateMachine, int priority, string stateName) : base(entity, stateMachine, priority, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _attackSkill = Player_SkillManager.Instance.Attack;

        Player_TimerManager.Instance.AddTimer(
            _attackSkill.AttackDuration,
            () => { SkillEvents.TriggerAttackEnd(); },
            "Player_AbilityTimer"
        );

        _player.AttributeSO.TargetVelocity = Vector2.zero;
        _player.Rb.gravityScale = _player.AttributeSO.AttackGravity;

        _player.AttributeSO.TargetVelocity = _player.InputSys.MouseDir *
            _attackSkill.AttackForce;
    }
    public override void PhysicsUpdate()
    {
        _player.Rb.linearVelocity = _player.AttributeSO.TargetVelocity;
    }
    public override void LogicUpdate()
    {

    }
    public override void Exit()
    {
        base.Exit();

        _player.AttributeSO.TargetVelocity = Vector2.zero;
        _attackSkill.CoolDownSkill();
        _player.Rb.gravityScale = _player.AttributeSO.FallGravity;
    }
}
