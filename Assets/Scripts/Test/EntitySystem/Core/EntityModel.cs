using System;

namespace ThisGame.EntitySystem
{
    public class EntityModel : IEntityModel
    {
        public string Id { get; set; }
        public float CurrentHealth { get; set; }
        public float MoveSpeed { get; set; }

        public bool IsAlive { get; set; }
        public bool IsBusy { get; set; }
        public bool IsPhysicsDriven { get; set; }

        // Dependency
        public IEntityData Data;
        public StateMachine StateMachine;
        public void Initialize(IEntityData data)
        {
            Data = data;
            StateMachine = new StateMachine();
            CurrentHealth = Data.MaxHealth;
        }
    }
}