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

        protected override Type[] SubscribeEvents => new Type[]
        {
            // Checkers
            typeof(GroundCheckChange),
            typeof(WallCheckChange),
            // Skills
            typeof(P_Skill_GrappingHookPressed),
            typeof(P_Skill_GrappingHookPrepare)
        };

        public override void Enter()
        {
            EventBus.Subscribe<JumpButtonPressed>(this, HandleJumpPressed);
        }

        public override void Exit()
        {
            EventBus.Unsubscribe<JumpButtonPressed>(HandleJumpPressed);
        }

        public override void LogicUpdate()
        {
            var wallCheck = _checkers.GetChecker<WallCheckModel>("WallCheckModel");
            var groundCheck = _checkers.GetChecker<GroundCheckModel>("GroundCheckModel");
            if (groundCheck.IsDetected || !wallCheck.IsDetected || _player.InputValue == Vector3.zero)
                _stateMachine.ChangeState("Idle");
        }

        public override void PhysicsUpdate()
        {
            _movement.SetVelocity(new Vector3(_movement.Velocity.x, _moveData.WallSlideSpeed, _movement.Velocity.z));
            _player.Rb.linearVelocity = _movement.Velocity;
        }
        void HandleJumpPressed(JumpButtonPressed jumpPressed)
        {
            _stateMachine.ChangeState("Jump");
            var jumpExecute = new JumpExecute
            {
                JumpDir = new Vector3(_moveData.WallJumpDirection.x * -_player.InputValue.x, _moveData.WallJumpDirection.y, _moveData.WallJumpDirection.z),
                EndEarly = true
            };
            EventBus.Publish(jumpExecute);
        }
        void HandleGrappingHookPressed(P_Skill_GrappingHookPressed e)
        {
            e.Skill.HandleSkillButtonPressed(e);
        }
        void HandleGrappingHookPrepare(P_Skill_GrappingHookPrepare e)
        {
            _stateMachine.ChangeState("Hooked");
            var grappingHookExecute = new P_Skill_GrappingHookExecuted()
            {
                Skill = _player.GetController<SkillController>().GetSkill<P_GrappingHookModel>("P_GrappingHook"),
                IsGrounded = false
            };
            EventBus.Publish(grappingHookExecute);
        }
    }
}