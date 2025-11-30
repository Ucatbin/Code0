using ThisGame.Core.CheckerSystem;
using ThisGame.Entity.SkillSystem;
using ThisGame.Entity.EntitySystem;
using ThisGame.Entity.MoveSystem;
using System;
using ThisGame.Core;
using UnityEngine;

namespace ThisGame.Entity.StateMachineSystem
{
    public class P_CoyotState : P_GroundState
    {
        public P_CoyotState(PlayerController entity, StateMachine stateMachine, string animName, CheckerController checkers, MoveModel movement) : base(entity, stateMachine, animName, checkers, movement)
        {
        }
        protected override Type[] AcceptedEvents => new Type[]
        {
            // Abilities
            typeof(JumpButtonPressed)
        };
        public override void Enter()
        {
            base.Enter();

            TimerManager.Instance.AddTimer(
                _movement.Data.CoyotTime,
                () => TryEnterAirState(),
                "Coyot Timer"
            );
        }

        public override void Exit()
        {
            base.Exit();

            TimerManager.Instance.CancelTimersWithTag("Coyot Timer");
        }

        public override void LogicUpdate()
        {
            _player.Rb.linearVelocity = _movement.Velocity;
        }

        public override void PhysicsUpdate()
        {
            _movement.HandleGravity(SmoothTime.FixedDeltaTime);
        }

        void TryEnterAirState()
        {
            var groundCheck = _checkers.GetChecker<GroundCheckModel>();
            if (!groundCheck.IsDetected)
            {
                _stateMachine.ChangeState<P_AirState>();
                var groundStateChanged = new GroundCheckChange()
                {
                    ChangeToGrounded = false,
                };
                EventBus.Publish(groundStateChanged);
            }
            else
                _stateMachine.ChangeState<P_IdleState>();
        }
    }
}