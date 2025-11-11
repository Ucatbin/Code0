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
        public P_GroundState(PlayerController entity, StateMachine stateMachine, CheckerController checkers, MoveModel movement) : base(entity, stateMachine, checkers, movement)
        {
        }
        protected override Type[] GetEvents() =>Array.Empty<Type>();
        
        public override void Enter()
        {
            base.Enter();

            _movement.SetVelocity(new Vector3(_movement.Velocity.x, 0f, _movement.Velocity.z));
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            _movement.UpdateMovement(_player.InputValue, Time.deltaTime);
            _player.Rb.linearVelocity = _movement.Velocity;
        }

        public override void PhysicsUpdate()
        {
        }
    }
}