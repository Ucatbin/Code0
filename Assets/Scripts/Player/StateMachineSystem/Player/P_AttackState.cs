using System;
using ThisGame.Core;
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
    P_AttackView _view;
    public P_AttackState(
        PlayerController entity,
        StateMachine stateMachine,
        string animName,
        CheckerController checkers,
        MoveModel movement,
        P_AttackModel skill
    ) : base(entity, stateMachine, animName, checkers, movement)
    {
        _skill = skill;
        _data = _skill.Data as P_AttackData;
        _view = SkillManager.Instance.GetSkillEntry(typeof(P_AttackModel)).View as P_AttackView;
    }

    protected override Type[] GetEvents() => new Type[]
    {

    };
    public override void Enter()
    {
        base.Enter();

        var attackDir = (_skill.InputDir - _player.transform.position).normalized;
        _view.HandleAttackView(attackDir);
        _movement.SetVelocity(Vector3.Scale(attackDir, _data.AttackForce));
        _player.Rb.linearVelocity = _movement.Velocity;
        if (attackDir.x * _player.FacingDir < 0)
        {
            var viewFlip = new ViewFlip()
            {
                FacingDir = -_player.FacingDir
            };
            EventBus.Publish(viewFlip);
        }

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

        _movement.SetVelocity(_player.Rb.linearVelocity);
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
