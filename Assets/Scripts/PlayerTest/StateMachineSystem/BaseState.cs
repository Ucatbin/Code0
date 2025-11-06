using ThisGame.Entity.EntitySystem;
using Unity.VisualScripting;

namespace ThisGame.Entity.StateMachineSystem
{
    public abstract class BaseState : IState
    {
        // Dependency
        protected EntityController _entity;
        protected StateMachine _stateMachine;
        public BaseState(EntityController entity, StateMachine stateMachine)
        {
            _entity = entity;
            _stateMachine = stateMachine;
        }

        public abstract void Enter();
        public abstract void PhysicsUpdate();
        public abstract void LogicUpdate();
        public abstract void Exit();
    }
}