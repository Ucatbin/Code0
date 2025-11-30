using System;
using ThisGame.Core;
using ThisGame.Core.CheckerSystem;
using ThisGame.Entity.EntitySystem;
using ThisGame.Entity.MoveSystem;
using UnityEngine;

namespace ThisGame.Entity.StateMachineSystem
{
    public class P_GroundState : P_BaseState
    {
        public P_GroundState(PlayerController entity, StateMachine stateMachine, string animName, CheckerController checkers, MoveModel movement) : base(entity, stateMachine, animName, checkers, movement)
        {
        }

        public override void Enter()
        {
            base.Enter();

            var groundStateChanged = new GroundCheckChange()
            {
                ChangeToGrounded = true,
            };
            EventBus.Publish(groundStateChanged);

            var velocity = _movement.Velocity;
            velocity.y = 0f;
            _movement.SetVelocity(velocity);
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            _movement.UpdateMovement(_player.InputValue, SmoothTime.DeltaTime);
            _player.Rb.linearVelocity = _movement.Velocity;
        }

        public override void PhysicsUpdate()
        {
        }
    }
}