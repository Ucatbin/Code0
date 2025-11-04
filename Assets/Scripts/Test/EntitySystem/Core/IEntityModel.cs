using UnityEngine;

namespace ThisGame.EntitySystem
{
    public interface IEntityModel
    {
        float CurrentHealth { get; set; }
        float MoveSpeed { get; set; }
        
        bool IsBusy { get; set; }
        bool IsPhysicsDriven { get; set; }
    }
}