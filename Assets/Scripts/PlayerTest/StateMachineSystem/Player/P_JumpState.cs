using System;
using ThisGame.Core.CheckerSystem;
using ThisGame.Entity.SkillSystem;
using ThisGame.Entity.EntitySystem;
using ThisGame.Entity.MoveSystem;
using UnityEngine;
using ThisGame.Core;

namespace ThisGame.Entity.StateMachineSystem
{
    public class P_JumpState : P_AirState
    {
        public P_JumpState(PlayerController entity, StateMachine stateMachine, string animName, CheckerController checkers, MoveModel movement) : base(entity, stateMachine, animName, checkers, movement)
        {
        }

        protected override Type[] GetEvents() => new Type[]
        {
            // Input
            typeof(JumpExecute),
            typeof(JumpButtonRelease),
            // Skills
            typeof(P_Skill_DoubleJumpExecute),
            typeof(P_Skill_GrappingHookPressed),
        };

        public override void Enter()
        {
            base.Enter();

            var moveData = _movement.Data as PlayerMoveData;
            TimerManager.Instance.AddTimer(
                moveData.JumpInputWindow,
                () => _stateMachine.ChangeState<P_AirState>(),
                "JumpStateTimer"
            );
        }

        public override void Exit()
        {
            base.Exit();
            TimerManager.Instance.CancelTimersWithTag("JumpStateTimer");
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            var moveData = _movement.Data as PlayerMoveData;
            _movement.SetVelocity(new Vector3(_movement.Velocity.x, moveData.BaseJumpSpeed, _movement.Velocity.z));
        }
    }
}