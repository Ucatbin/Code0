using UnityEngine;

namespace ThisGame.EntitySystem
{
    public interface IEntityModel
    {
        string Id { get; set; }
        float CurrentHealth { get; set; }
        float MoveSpeed { get; set; }
        
        bool IsAlive{ get; set; }
        bool IsBusy { get; set; }
        bool IsPhysicsDriven { get; set; }

        void Initialize(IEntityData data);
    }
}