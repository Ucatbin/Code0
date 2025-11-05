using UnityEngine;

public interface IAbilityData
{
    public string AbilityName { get; }
    public int AbilityHash { get; }
    public int MaxLevel { get; }
    public float CoolDown { get; }
    public int MaxCharges { get; }
    public Sprite Icon { get; }
}
