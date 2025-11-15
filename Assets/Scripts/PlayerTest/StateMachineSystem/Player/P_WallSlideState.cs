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
            var moveData = _movement.Data as PlayerMoveData;
            var maxFallSpeed = moveData.WallSlideSpeed;
            var velocity = _movement.Velocity;
            velocity.y = Mathf.MoveTowards(
                velocity.y,
                maxFallSpeed,
                moveData.WallSlideAcceleration * Time.fixedDeltaTime
            );
            _movement.SetVelocity(velocity);
            _player.Rb.linearVelocity = _movement.Velocity;
        }
        protected override void HandleJumpPressed(JumpButtonPressed @event)
        {
            _stateMachine.ChangeState<P_JumpState>();
            var moveData = _movement.Data as PlayerMoveData;
            var jumpExecute = new JumpExecute
            {
                JumpType = JumpType.WallJump,
                JumpDir = moveData.WallJumpDirection * -_player.FacingDir
            };
            EventBus.Publish(jumpExecute);
        }
    }
}