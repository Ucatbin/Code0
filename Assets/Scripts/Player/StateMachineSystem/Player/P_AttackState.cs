using System;
using ThisGame.Core.CheckerSystem;
using ThisGame.Entity.EntitySystem;
using ThisGame.Entity.MoveSystem;
using ThisGame.Entity.SkillSystem;
using ThisGame.Entity.StateMachineSystem;
using UnityEngine;

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

    };
    public override void Enter()
    {
        base.Enter();

        TimerManager.Instance.AddTimer(
            _data.AttackDuration,
            () => {
                _stateMachine.ChangeState<P_IdleState>();
            },
            "Player_AbilityTimer"
        );
    }
    public override void Exit()
    {
        base.Exit();

        _skill.StartCoolDown();
    }
    public override void LogicUpdate()
    {
        _movement.UpdateMovement(Vector3.zero, Time.deltaTime);
        _player.Rb.linearVelocity = _movement.Velocity;
    }
    public override void PhysicsUpdate()
    {
        _movement.HandleGravity(Time.fixedDeltaTime);
    }
}
