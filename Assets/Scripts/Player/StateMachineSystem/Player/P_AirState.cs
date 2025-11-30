using System;
using ThisGame.Core.CheckerSystem;
using ThisGame.Entity.EntitySystem;
using ThisGame.Entity.MoveSystem;
using ThisGame.Entity.SkillSystem;
using UnityEngine;

namespace ThisGame.Entity.StateMachineSystem
{
    public class P_AirState : P_BaseState
    {
        public P_AirState(PlayerController entity, StateMachine stateMachine, string animName, CheckerController checkers, MoveModel movement) : base(entity, stateMachine, animName, checkers, movement)
        {
        }

        protected override Type[] AcceptedEvents => new Type[]
        {
            typeof(JumpExecute),
            typeof(P_SkillPressed),
            typeof(P_SkillStateSwitch),
        };
        protected override Type[] AcceptedSkillPressEvents => new Type[]
        {
            typeof(P_AttackModel),
            typeof(P_GrappingHookModel),
            typeof(P_DashAttackModel),
            typeof(P_DoubleJumpModel)
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
            _movement.UpdateMovement(_player.InputValue, SmoothTime.DeltaTime);
            _player.Rb.linearVelocity = _movement.Velocity;

            var wallCheck = _checkers.GetChecker<WallCheckModel>();
            if (wallCheck.IsDetected &&
                _player.Rb.linearVelocityY <= 0 &&
                _player.InputValue != Vector3.zero
            )
                _stateMachine.ChangeState<P_WallSlideState>();

            var groundCheck = _checkers.GetChecker<GroundCheckModel>();
            if (groundCheck.IsDetected && _player.Rb.linearVelocityY <= 0)
                _stateMachine.ChangeState<P_IdleState>();

            _player.View.Animator.SetFloat("velocityY", _player.Rb.linearVelocityY);
        }

        public override void PhysicsUpdate()
        {
            _movement.HandleGravity(SmoothTime.FixedDeltaTime);
        }
    }
}