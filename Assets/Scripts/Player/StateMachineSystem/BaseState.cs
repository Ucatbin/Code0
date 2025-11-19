using ThisGame.Entity.EntitySystem;
using Unity.VisualScripting;

namespace ThisGame.Entity.StateMachineSystem
{
    public abstract class BaseState : IState
    {
        // Dependency
        protected EntityController _entity;
        protected StateMachine _stateMachine;
        public string AnimName;
        public BaseState(EntityController entity, StateMachine stateMachine, string animName)
        {
            _entity = entity;
            _stateMachine = stateMachine;
            AnimName = animName;
        }

        public abstract void Enter();
        public abstract void PhysicsUpdate();
        public abstract void LogicUpdate();
        public abstract void Exit();
    }
}