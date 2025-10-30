using System;
using CharacterSystem;

[Serializable]
public class EntityModel
{
    // Info
    public float CurrentHealth;
    // State
    public bool IsBusy;
    public bool IsWallSliding;
    public bool IsPhysicsDriven;

    // Dependency
    public readonly EntityData Data;
    public EntityModel(EntityData data)
    {
        Data = data;
        ServiceLocator.Register(this);
    }
}