using ThisGame.Core;
using ThisGame.Entity.EntitySystem;
using ThisGame.Entity.MoveSystem;
using UnityEngine;

namespace ThisGame.Entity.StateMachineSystem
{
    public class P_GroundState : P_BaseState
    {
        protected MoveModel _movement;
        protected Vector3 _currentInput;

        public P_GroundState(PlayerController entity, StateMachine stateMachine, MoveModel model) : base(entity, stateMachine)
        {
            _movement = model;

            EventBus.Subscribe<MoveButtonPressed>(this, OnMovePressed);
            EventBus.Subscribe<JumpButtonPressed>(this, OnJumpPressed);
        }
        void OnMovePressed(MoveButtonPressed moveInputInfo)
        {
            _currentInput = moveInputInfo.MoveDirection;
        }
        void OnJumpPressed(JumpButtonPressed jumpPressed)
        {
            _stateMachine.ChangeState("Jump");
        }

        public override void Enter()
        {
            
        }

        public override void Exit()
        {
            
        }

        public override void LogicUpdate()
        {
            
        }

        public override void PhysicsUpdate()
        {
            _movement.UpdateMovement(_currentInput, Time.fixedDeltaTime);
            _player.Rb.linearVelocity = _movement.Velocity;
        }
    }
}