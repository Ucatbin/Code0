using UnityEngine;

public class Player_AttackState : Player_BaseState
{
    public Player_AttackState(PlayerController entity, StateMachine stateMachine, string stateName) : base(entity, stateMachine, stateName)
    {
    }
    public override void Enter()
    {
        _player.IsAttacking = true;
        _player.Rb.gravityScale = 0f;
        Player_TimerManager.Instance.AddTimer(
            Player_SkillManager.Instance.Attack.AttackDuration,
            () => { _stateMachine.ChangeState(_player.IdleState, true); },
            "Player_AbilityTimer"
        );
    }
    public override void PhysicsUpdate() { }
    public override void LogicUpdate() { }
    public override void Exit()
    {
        Player_SkillManager.Instance.Attack.CoolDownSkill();
        _player.IsAttacking = false;
        _player.Rb.gravityScale = _player.AttributeSO.DefaultGravity;
    }
}
