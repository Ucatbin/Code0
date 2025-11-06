using ThisGame.Core;
using ThisGame.Entity.EntitySystem;
using ThisGame.Entity.MoveSystem;
using UnityEngine;

namespace ThisGame.Entity.StateMachineSystem
{
    public class P_IdleState : EntityBaseState
    {
        MoveModel _moveModel;
        EntityController _entity;
        Vector3 _currentInput;
        public P_IdleState(EntityController entity, MoveModel model) : base(entity)
        {
            _entity = entity;
            _moveModel = model;
        }

        public override void Enter()
        {
            EventBus.Subscribe<PlayerMoveInputEvent>(this, OnMoveInput);
        }

        public override void Exit()
        {

        }

        public override void LogicUpdate()
        {

        }

        public override void PhysicsUpdate()
        {
            _moveModel.UpdateMovement(_currentInput, Time.fixedDeltaTime);
        }

        void OnMoveInput(PlayerMoveInputEvent moveEvent)
        {
            _currentInput = moveEvent.MoveDirection;
        }
    }
}