using System;
using ThisGame.Core;
using ThisGame.Core.CheckerSystem;
using ThisGame.Entity.EntitySystem;
using ThisGame.Entity.MoveSystem;
using ThisGame.Entity.SkillSystem;

namespace ThisGame.Entity.StateMachineSystem
{
    public class P_BaseState : BaseState
    {
        protected PlayerController _player;
        protected CheckerController _checkers;
        protected MoveModel _movement;
        protected PlayerMoveData _moveData;
        protected virtual Type[] SubscribeEvents => new Type[0];
        
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

        protected virtual void SubscribeAllEvents()
        {
            foreach (var eventType in SubscribeEvents)
            {
                EventBus.SubscribeByType(this, eventType);
            }
        }
        protected virtual void UnsubscribeAllEvents()
        {
            EventBus.UnsubscribeAll(this);
        }
    }
}