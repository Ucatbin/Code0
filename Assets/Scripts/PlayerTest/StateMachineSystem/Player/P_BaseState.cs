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
        protected PlayerMoveData _moveData;
        protected Dictionary<Type, Action<ISkillEvent>> _skillHandlers;
        
        public P_BaseState(
            PlayerController entity,
            StateMachine stateMachine,
            CheckerController checkers,
            MoveModel movement
        ) : base(entity, stateMachine)
        {
            _player = entity;
            _checkers = checkers;
            _movement = movement;
            _moveData = _movement.Data as PlayerMoveData;
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

            Debug.Log($"📢 {GetType().Name} Suscribed {events.Length} events");
        }
        protected virtual void UnsubscribeAllEvents()
        {
            EventBus.UnsubscribeAll(this);
        }
        #region Input
        // Move
        protected virtual void HandleMovePressed(MoveButtonPressed @event)
        {
            _stateMachine.ChangeState("Move");
        }
        protected virtual void HandleMoveRelease(MoveButtonRelease @event)
        {
            _stateMachine.ChangeState("Idle");
        }
        // Jump
        protected virtual void HandleJumpPressed(JumpButtonPressed @event)
        {
                _stateMachine.ChangeState("Jump");
                var jumpExecute = new JumpExecute
                {
                    JumpDir = new Vector3(0f, 1f, 0f),
                    EndEarly = false
                };
                EventBus.Publish(jumpExecute);
        }
        protected virtual void HandleJumpRelease(JumpButtonRelease @event)
        {
            _stateMachine.ChangeState("Air");
        }
        #endregion
        #region Skills
        // DoubleJump
        protected virtual void HandleDoubleJumpPressed(P_Skill_DoubleJumpPressed @event)
        {
            @event.Skill.HandleSkillButtonPressed(@event);
        }
        protected virtual void HandleDoubleJumpPrepare(P_Skill_DoubleJumpPrepare @event)
        {
            _stateMachine.ChangeState("Jump");
            var jumpData = @event.Skill.Data as P_DoubleJumpData;
            var doubleJumpExecute = new P_Skill_DoubleJumpExecute()
            {
                DoubleJumpSpeed = jumpData.JumpSpeed
            };
            EventBus.Publish(doubleJumpExecute);
        }
        protected virtual void HandleDoubleJumpExecute(P_Skill_DoubleJumpExecute @event)
        {
            _movement.SetVelocity(new Vector3(_movement.Velocity.x, @event.DoubleJumpSpeed, _movement.Velocity.z));
            _player.Rb.linearVelocity = _movement.Velocity;
            _stateMachine.ChangeState("Air");
        }

        // GrappingHook
        protected virtual void HandleGrappingHookPressed(P_Skill_GrappingHookPressed @event)
        {
            @event.Skill.HandleSkillButtonPressed(@event);
        }
        protected virtual void HandleGrappingHookPrepare(P_Skill_GrappingHookPrepare @event)
        {
            _stateMachine.ChangeState("Hooked");
            var grappingHookExecute = new P_Skill_GrappingHookExecuted()
            {
                Skill = _player.GetController<SkillController>().GetSkill<P_GrappingHookModel>(),
                IsGrounded = false
            };
            EventBus.Publish(grappingHookExecute);
        }
        protected virtual void HandleGrappingHookExecute(P_Skill_GrappingHookExecuted @event)
        {
        }
        protected virtual void HandleGrappingHookRelease(P_Skill_GrappingHookReleased @event)
        {
            _player.Joint.enabled = false;
            _stateMachine.ChangeState("Idle");
        }
        #endregion
        #endregion
    }
}