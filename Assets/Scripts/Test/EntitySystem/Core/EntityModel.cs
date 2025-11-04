using System;

namespace ThisGame.EntitySystem
{
    public class EntityModel<TData> : IEntityModel where TData : EntityData
    {
        public float CurrentHealth { get; set; }
        public float MoveSpeed { get; set; }

        public bool IsBusy { get; set; }
        public bool IsPhysicsDriven { get; set; }

        // Dependency
        public readonly EntityData Data;
        public readonly StateMachine StateMachine;
        public EntityModel(EntityData data)
        {
            Data = data;
            StateMachine = new StateMachine();
            ServiceLocator.Register(this);
        }
    }
}