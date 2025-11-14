using ThisGame.Core;
using ThisGame.Core.CheckerSystem;
using ThisGame.Entity.SkillSystem;
using ThisGame.Entity.EntitySystem;
using ThisGame.Entity.MoveSystem;
using UnityEngine;
using System;

namespace ThisGame.Entity.StateMachineSystem
{
    public class P_WallSlideState : P_BaseState
    {
        public P_WallSlideState(PlayerController entity, StateMachine stateMachine, string animName, CheckerController checkers, MoveModel movement) : base(entity, stateMachine, animName, checkers, movement)
        {
        }

        protected override Type[] GetEvents() => new Type[]
        {
            // Abilities
            typeof(JumpButtonPressed),
            // Skills
            typeof(P_Skill_AttackPressed),
            typeof(P_Skill_AttackExecute),
            typeof(P_Skill_GrappingHookPressed),
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
            var wallCheck = _checkers.GetChecker<WallCheckModel>();
            var groundCheck = _checkers.GetChecker<GroundCheckModel>();
            if (groundCheck.IsDetected || !wallCheck.IsDetected || _player.InputValue == Vector3.zero)
                _stateMachine.ChangeState<P_IdleState>();
        }

        public override void PhysicsUpdate()
        {
            _movement.SetVelocity(new Vector3(_movement.Velocity.x, _moveData.WallSlideSpeed, _movement.Velocity.z));
            _player.Rb.linearVelocity = _movement.Velocity;
        }
        protected override void HandleJumpPressed(JumpButtonPressed @event)
        {
            if (@event is JumpButtonPressed inputEvent)
            {
                _stateMachine.ChangeState<P_JumpState>();
                var jumpExecute = new JumpExecute
                {
                    JumpDir = new Vector3(_moveData.WallJumpDirection.x * -_player.InputValue.x, _moveData.WallJumpDirection.y, _moveData.WallJumpDirection.z),
                    EndEarly = true
                };
                EventBus.Publish(jumpExecute);
            }
        }
    }
}