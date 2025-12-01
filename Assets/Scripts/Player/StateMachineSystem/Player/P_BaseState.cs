using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
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
        protected virtual Type[] AcceptedEvents => Array.Empty<Type>();
        protected virtual Type[] AcceptedSkillPressEvents => Array.Empty<Type>();
        protected virtual Type[] AcceptedSkillExecuteEvents => Array.Empty<Type>();

        protected virtual void SubscribeAllEvents()
        {
            var events = AcceptedEvents;

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
            _player.View.Animator.SetBool("Air", true);
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
        protected virtual void HandleBeHit(BeHit @event)
        {
            _stateMachine.ChangeState<P_BeHitState>();
        }
        #endregion
        #region Skills
        protected virtual void HandleSkillPressed(P_SkillPressed @event)
        {
            if (!AcceptedSkillPressEvents.Any(t => t == @event.Skill.GetType())) return;
            switch (@event.Skill)
            {
                case P_AttackModel attack:
                    attack.HandleSkillButtonPressed(@event);
                    break;
                case P_DashAttackModel dashAttack:
                    dashAttack.HandleSkillButtonPressed(@event);
                    break;
                case P_DoubleJumpModel doubleJump:
                    doubleJump.HandleSkillButtonPressed(@event);
                    break;
                case P_GrappingHookModel ghook:
                    ghook.HandleSkillButtonPressed(@event);
                    break;
            }
        }
        protected virtual void HandleSkillExecute(P_SkillExecute @event)
        {
            if (!AcceptedSkillExecuteEvents.Any(t => t == @event.Skill.GetType())) return;
            switch (@event.Skill)
            {
                case P_DashAttackModel dashAttack:
                    dashAttack.ExecuteSkill(@event);
                    break;
            }
        }
        protected virtual void HandleSkillReleased(P_SkillReleased @event)
        {
            if (!AcceptedSkillPressEvents.Any(t => t == @event.Skill.GetType())) return;
            switch (@event.Skill)
            {
                case P_AttackModel attack:
                    attack.HandleSkillButtonReleased(@event);
                    break;
                case P_DashAttackModel dashAttack:
                    dashAttack.HandleSkillButtonReleased(@event);
                    break;
                case P_DoubleJumpModel doubleJump:
                    doubleJump.HandleSkillButtonReleased(@event);
                    break;
                case P_GrappingHookModel ghook:
                    ghook.HandleSkillButtonReleased(@event);
                    break;
            }
        }
        protected virtual void HandleSwitchSkillState(P_SkillStateSwitch @event)
        {
            _stateMachine.ChangeState(@event.SkillState);
        }
        #endregion
        #endregion
    }
}