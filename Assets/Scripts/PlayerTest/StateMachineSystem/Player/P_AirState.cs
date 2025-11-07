using System.Data;
using ThisGame.Core;
using ThisGame.Entity.EntitySystem;
using ThisGame.Entity.MoveSystem;
using UnityEngine;

namespace ThisGame.Entity.StateMachineSystem
{
    public class P_AirState : P_BaseState
    {
        protected MoveModel _movement;
        protected Vector3 _currentInput;

        public P_AirState(PlayerController entity, StateMachine stateMachine, MoveModel model) : base(entity, stateMachine)
        {
            _movement = model;

            EventBus.Subscribe<MoveButtonPressed>(this, OnMovePressed);
        }
        void OnMovePressed(MoveButtonPressed moveInputInfo)
        {
            _currentInput = moveInputInfo.MoveDirection;
        }

        public override void Enter()
        {
            
        }

        public override void Exit()
        {
            
        }

        public override void LogicUpdate()
        {
            Debug.Log(_player.IsGrounded);
            if (_player.IsGrounded)
                _stateMachine.ChangeState("Idle");
        }

        public override void PhysicsUpdate()
        {
            _movement.UpdateMovement(_currentInput, Time.fixedDeltaTime);
            _movement.HandleGravity(Time.fixedDeltaTime);
            _player.Rb.linearVelocity = _movement.Velocity;
        }
    }
}