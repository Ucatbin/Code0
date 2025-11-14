using System;
using ThisGame.Core.CheckerSystem;
using ThisGame.Entity.EntitySystem;
using ThisGame.Entity.MoveSystem;
using ThisGame.Entity.SkillSystem;
using ThisGame.Entity.StateMachineSystem;

public class P_AttackState : P_BaseState
{
    P_AttackModel _skill;
    P_AttackData _data;
    public P_AttackState(PlayerController entity, StateMachine stateMachine, string animName, CheckerController checkers, MoveModel movement, P_AttackModel skill) : base(entity, stateMachine, animName, checkers, movement)
    {
        _skill = skill;
        _data = _skill.Data as P_AttackData;
    }

    protected override Type[] GetEvents() => new Type[]
    {
        typeof(P_Skill_AttackExecute)
    };
    public override void Enter()
    {
        base.Enter();

        var data = _skill.Data as P_AttackData;
        TimerManager.Instance.AddTimer(
            data.AttackDuration,
            () => {
                _stateMachine.ChangeState<P_IdleState>();
            },
            "Player_AbilityTimer"
        );
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void LogicUpdate() { }
    public override void PhysicsUpdate() { }

    protected override void HandleAttackExecute(P_Skill_AttackExecute @event)
    {
    }
}
