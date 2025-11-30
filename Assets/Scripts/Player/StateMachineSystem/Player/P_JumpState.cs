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
        bool _canHold;
        public P_JumpState(PlayerController entity, StateMachine stateMachine, string animName, CheckerController checkers, MoveModel movement) : base(entity, stateMachine, animName, checkers, movement)
        {
        }

        protected override Type[] AcceptedEvents => new Type[]
        {
            // Input
            typeof(JumpExecute),
            typeof(JumpButtonRelease),
            // Skills
            typeof(P_SkillPressed),
            typeof(P_SkillStateSwitch),
        };
        protected override Type[] AcceptedSkillPressEvents => new Type[]
        {
            typeof(P_GrappingHookModel)
        };

        public override void Enter()
        {
            base.Enter();

            var moveData = _movement.Data as PlayerMoveData;
            TimerManager.Instance.AddTimer(
                moveData.JumpInputWindow,
                () => _stateMachine.ChangeState<P_AirState>(),
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
            
            if (!_canHold) return;

            var moveData = _movement.Data as PlayerMoveData;
            var velocity = _movement.Velocity;
            velocity.y = moveData.BaseJumpSpeed;
            _movement.SetVelocity(velocity);
        }

        protected override void HandleJumpExecute(JumpExecute @event)
        {
            base.HandleJumpExecute(@event);

            switch (@event.JumpType)
            {
                case JumpType.Jump:
                    _canHold = true;
                    break;
                case JumpType.WallJump:
                    _canHold = false;
                    break;
                case JumpType.DoubleJump:
                    _canHold = false;
                    break;
            }
        }
    }
}