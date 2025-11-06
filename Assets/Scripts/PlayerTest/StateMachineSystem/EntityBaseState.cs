
using ThisGame.Entity.EntitySystem;

namespace ThisGame.Entity.StateMachineSystem
{
    public abstract class EntityBaseState : IState
    {
        // Dependency
        EntityController _entity;
        public EntityBaseState(EntityController entity)
        {
            _entity = entity;
        }
        public abstract void Enter();
        public abstract void Exit();
        public abstract void LogicUpdate();
        public abstract void PhysicsUpdate();
    }
}