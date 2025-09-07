using UnityEngine;

public class Player_AttackState : Player_BaseState
{
    PlayerSkill_Attack _attack;
    public Player_AttackState(PlayerController entity, StateMachine stateMachine, string stateName) : base(entity, stateMachine, stateName)
    {
    }
    public override void Enter()
    {
        base.Enter();

        _attack = Player_SkillManager.Instance.Attack;

        Player_TimerManager.Instance.AddTimer(
            _attack.AttackDuration,
            () => { SkillEvents.TriggerAttackEnd(); },
            "Player_AbilityTimer"
        );

        _player.AttributeSO.TargetVelocity = Vector2.zero;
        _player.Rb.gravityScale = _player.AttributeSO.AttackGravity;

        _player.AttributeSO.TargetVelocity = _player.InputSys.MouseDir *
            _attack.AttackForce;
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
        _attack.CoolDownSkill();
        _player.Rb.gravityScale = _player.AttributeSO.MaxFallGravity;
    }
}
