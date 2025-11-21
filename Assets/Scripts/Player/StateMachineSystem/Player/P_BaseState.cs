using System;
using System.Collections.Generic;
using ThisGame.Core;
using ThisGame.Core.CheckerSystem;
using ThisGame.Entity.EntitySystem;
using ThisGame.Entity.MoveSystem;
using ThisGame.Entity.SkillSystem;
using UnityEngine;

namespace ThisGame.Entity.StateMachineSystem
{
    public abstract class P_BaseState : BaseState
    {
        protected PlayerController _player;
        protected CheckerController _checkers;
        protected MoveModel _movement;
        
        public P_BaseState(
            PlayerController entity,
            StateMachine stateMachine,
            string animName,
            CheckerController checkers,
            MoveModel movement
        ) : base(entity, stateMachine, animName)
        {
            _player = entity;
            _checkers = checkers;
            _movement = movement;
        }

        public override void Enter()
        {
            SubscribeAllEvents();
        }
        public override void Exit()
        {
            UnsubscribeAllEvents();
        }
        public override void LogicUpdate() { }
        public override void PhysicsUpdate() { }

        #region Events
        protected abstract Type[] GetEvents();
        protected virtual void SubscribeAllEvents()
        {
            var events = GetEvents();

            foreach (var eventType in events)
                EventBus.SubscribeByType(this, eventType);
        }
        protected virtual void UnsubscribeAllEvents()
        {
            EventBus.UnsubscribeAll(this);
        }
        #region Input
        // Move
        protected virtual void HandleMovePressed(MoveButtonPressed @event)
        {
            _stateMachine.ChangeState<P_MoveState>();
        }
        protected virtual void HandleMoveRelease(MoveButtonRelease @event)
        {
            _stateMachine.ChangeState<P_IdleState>();
        }
        // Jump
        protected virtual void HandleJumpPressed(JumpButtonPressed @event)
        {
            _stateMachine.ChangeState<P_JumpState>();
            var jumpExecute = new JumpExecute
            {
                JumpType = JumpType.Jump,
                JumpDir = new Vector3(0f, 1f, 0f),
            };
            EventBus.Publish(jumpExecute);
        }
        protected virtual void HandleJumpExecute(JumpExecute @event)
        {
            var animator = _player.View.Animator;
            var moveData = _movement.Data as PlayerMoveData;
            var velocity = _movement.Velocity;
            Vector3 jumpSpeed = @event.JumpDir * moveData.BaseJumpSpeed;
            switch (@event.JumpType)
            {
                case JumpType.Jump:
                    animator.SetInteger("JumpType", 0);
                    velocity.y = jumpSpeed.y;
                    _movement.SetVelocity(velocity);
                    _player.Rb.linearVelocity = _movement.Velocity;
                    break;
                case JumpType.WallJump:
                    animator.SetInteger("JumpType", 1);
                    _movement.SetVelocity(jumpSpeed);
                    break;
                case JumpType.DoubleJump:
                    animator.SetInteger("JumpType", 0);
                    velocity.y = jumpSpeed.y;
                    _movement.SetVelocity(velocity);
                    _player.Rb.linearVelocity = _movement.Velocity;
                    _stateMachine.ChangeState<P_AirState>();
                    break;
            }
        }
        protected virtual void HandleJumpRelease(JumpButtonRelease @event)
        {
            _stateMachine.ChangeState<P_AirState>();
        }
        #endregion
        #region State
        #endregion
        #region Skills
        // Attack
        protected virtual void HandleAttackPressed(P_Skill_AttackPressed @event)
        {
            @event.Skill.HandleSkillButtonPressed(@event);
        }
        protected virtual void HandleAttackExecute(P_Skill_AttackExecute @event)
        {
            if ((@event.AttackDirection.x - _player.transform.position.x) * _player.FacingDir < 0)
            {
                var viewFlip = new ViewFlip()
                {
                    FacingDir = -_player.FacingDir
                };
                EventBus.Publish(viewFlip);
            }
            _stateMachine.ChangeState<P_AttackState>();
        }
        // DoubleJump
        protected virtual void HandleDoubleJumpPressed(P_Skill_DoubleJumpPressed @event)
        {
            @event.Skill.HandleSkillButtonPressed(@event);
        }
        protected virtual void HandleDoubleJumpExecute(P_Skill_DoubleJumpExecute @event)
        {
            _stateMachine.ChangeState<P_JumpState>();
            var jumpExecute = new JumpExecute()
            {
                JumpType = JumpType.DoubleJump,
                JumpDir = new Vector3(0f, @event.DoubleJumpSpeed, 0f),
            };
            EventBus.Publish(jumpExecute);
        }

        // GrappingHook
        protected virtual void HandleGrappingHookPressed(P_Skill_GrappingHookPressed @event)
        {
            @event.Skill.HandleSkillButtonPressed(@event);
        }
        protected virtual void HandleGrappingHookExecute(P_Skill_GrappingHookExecute @event)
        {
            _stateMachine.ChangeState<P_HookedState>();
        }
        protected virtual void HandleGrappingHookRelease(P_Skill_GrappingHookReleased @event)
        {
            _player.Joint.enabled = false;
            _stateMachine.ChangeState<P_IdleState>();
        }
        #endregion
        #endregion
    }
}