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
        public P_WallSlideState(PlayerController entity, StateMachine stateMachine, CheckerController checkers, MoveModel movement) : base(entity, stateMachine, checkers, movement)
        {
        }
        protected override Type[] GetInputEvents() => new Type[]
        {
            typeof(JumpButtonPressed)
        };
        protected override Type[] GetSkillEvents() => new Type[]
        {
            typeof(P_Skill_GrappingHookPressed),
            typeof(P_Skill_GrappingHookPrepare)
        };

        public override void Enter()
        {
        }

        public override void Exit()
        {

        }

        public override void LogicUpdate()
        {
            var wallCheck = _checkers.GetChecker<WallCheckModel>();
            var groundCheck = _checkers.GetChecker<GroundCheckModel>();
            if (groundCheck.IsDetected || !wallCheck.IsDetected || _player.InputValue == Vector3.zero)
                _stateMachine.ChangeState("Idle");
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
                _stateMachine.ChangeState("Jump");
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