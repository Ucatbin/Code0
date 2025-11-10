using System;
using ThisGame.Core.CheckerSystem;
using ThisGame.Entity.SkillSystem;
using ThisGame.Entity.EntitySystem;
using ThisGame.Entity.MoveSystem;
using UnityEngine;
using ThisGame.Core;

namespace ThisGame.Entity.StateMachineSystem
{
    public class P_AirState : P_BaseState
    {
        public P_AirState(PlayerController entity, StateMachine stateMachine, CheckerController checkers, MoveModel movement) : base(entity, stateMachine, checkers, movement)
        {
        }
        protected override Type[] GetInputEvents() => Array.Empty<Type>();
        protected override Type[] GetSkillEvents() => new Type[]
        {
            typeof(P_Skill_DoubleJumpPressed),
            typeof(P_Skill_DoubleJumpPrepare),
            typeof(P_Skill_GrappingHookPressed),
            typeof(P_Skill_GrappingHookPrepare)
        };

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            _movement.UpdateMovement(_player.InputValue, Time.deltaTime);
            _player.Rb.linearVelocity = _movement.Velocity;

            var wallCheck = _checkers.GetChecker<WallCheckModel>();
            if (wallCheck.IsDetected &&
                _player.Rb.linearVelocityY <= 0 &&
                _player.InputValue != Vector3.zero
            )
                _stateMachine.ChangeState("WallSlide");

            var groundCheck = _checkers.GetChecker<GroundCheckModel>();
            if (groundCheck.IsDetected && _player.Rb.linearVelocityY <= 0)
                _stateMachine.ChangeState("Idle");
        }

        public override void PhysicsUpdate()
        {
            _movement.HandleGravity(Time.fixedDeltaTime);
        }
    }
}