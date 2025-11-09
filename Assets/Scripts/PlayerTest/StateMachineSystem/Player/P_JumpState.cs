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
        public P_JumpState(PlayerController entity, StateMachine stateMachine, CheckerController checkers, MoveModel movement) : base(entity, stateMachine, checkers, movement)
        {
        }

        protected override Type[] SubscribeEvents => new Type[]
        {
            // Movement
            typeof(JumpExecute),
            typeof(JumpButtonReleased),
            // Skills
            typeof(P_Skill_DoubleJumpExecuted),
            typeof(P_Skill_GrappingHookPressed),
            typeof(P_Skill_GrappingHookPrepare)
        };

        public override void Enter()
        {
            base.Enter();

            TimerManager.Instance.AddTimer(
                _moveData.JumpInputWindow,
                () => _stateMachine.ChangeState("Air"),
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

            _movement.SetVelocity(new Vector3(_movement.Velocity.x, _moveData.BaseJumpSpeed, _movement.Velocity.z));
        }

        void HandleJump(JumpExecute e)
        {
            if (e.EndEarly)
            {
                _movement.SetVelocity(e.JumpDir * _moveData.BaseJumpSpeed);
                _player.Rb.linearVelocity = _movement.Velocity;
                _stateMachine.ChangeState("Air");
            }
            else
            {
                var jumpSpeed = e.JumpDir.y * _moveData.BaseJumpSpeed;
                _movement.SetVelocity(new Vector3(_movement.Velocity.x, jumpSpeed, _movement.Velocity.z));
                _player.Rb.linearVelocity = _movement.Velocity;
            }
        }
        void HandleJumpEnd(JumpButtonReleased e)
        {
            _stateMachine.ChangeState("Air");
        }
        void HandleDoubleJump(P_Skill_DoubleJumpExecuted e)
        {
            _movement.SetVelocity(new Vector3(_movement.Velocity.x, e.DoubleJumpSpeed, _movement.Velocity.z));
            _player.Rb.linearVelocity = _movement.Velocity;
            _stateMachine.ChangeState("Air");
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