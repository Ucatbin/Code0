using UnityEngine;

public interface IEntityData
{
    public string EntityName { get; }
    [HideInInspector] public int EntityHash { get; }
    public float MaxHealth { get; }
    public float BaseSpeed { get; }
}
