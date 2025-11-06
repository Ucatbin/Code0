using ThisGame.Core;
using ThisGame.Entity.EntitySystem;
using ThisGame.Entity.MoveSystem;
using UnityEngine;

namespace ThisGame.Entity.StateMachineSystem
{
    public class P_MoveState : BaseState
    {
        PlayerController _player;
        MoveModel _movement;
        Vector3 _currentInput;

        public P_MoveState(PlayerController entity, StateMachine stateMachine, MoveModel model) : base(entity, stateMachine)
        {
            _player = entity;
            _movement = model;
            EventBus.Subscribe<PlayerMoveInputEvent>(this, OnMoveInput);
        }

        void OnMoveInput(PlayerMoveInputEvent moveEvent)
        {
            _currentInput = moveEvent.MoveDirection;
        }

        public override void Enter()
        {
        }

        public override void PhysicsUpdate()
        {
            _movement.UpdateMovement(_currentInput, Time.fixedDeltaTime);
            _player.Rb.linearVelocity = _movement.Velocity;
        }
        public override void LogicUpdate()
        {
            if (!_movement.IsMoving)
                _stateMachine.ChangeState("Idle");
        }

        public override void Exit()
        {
        }
    }
}