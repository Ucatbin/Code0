using System;
using UnityEngine;

#region Data
[CreateAssetMenu(fileName = "NewBuff_BuffData", menuName = "Game/BuffSys/BaseBuffBuilder/Data")]
public class BuffDataSO : ScriptableObject
{
    [Header("Basic Infos")]
    public int Id;
    public string BuffName;
    public string Description;
    public Sprite Icon;
    public int Priority;
    public int MaxStacks;
    public string[] Tags;
    [Header("Time Info")]
    public float Duration;  // The duration of buff
    public float Interval;  // How many tick will buff last
    [Header("Update")]
    public BuffType BuffType;
    public BuffStackType BuffStackType;
    public BuffRemoveType BuffRemoveType;
    [Header("Call Back")]
    /// <summary>
    /// Invoke when buff first added
    /// </summary>
    public BaseBuffModifier OnCreat;
    /// <summary>
    /// Invoke when buff just removed
    /// </summary>
    public BaseBuffModifier OnRemove;
    /// <summary>
    /// Invoke when buff should be handle based on timer
    /// </summary>
    public BaseBuffModifier OnInvoke;

    /// <summary>
    /// Invoke when hit other
    /// </summary>
    public BaseBuffModifier OnHit;
    /// <summary>
    /// Invoke when be hit
    /// </summary>
    public BaseBuffModifier OnBeHit;
    /// <summary>
    /// Invoke when killed other
    /// </summary>
    public BaseBuffModifier OnKill;
    /// <summary>
    /// Invoke when be killed
    /// </summary>
    public BaseBuffModifier OnBekill;
}
#endregion

#region Item
[Serializable]
public class BaseBuffItem : IComparable<BaseBuffItem>
{
    public BuffDataSO BuffData; // Get the data of buffSO
    public EntityControllerOld Source;        // Who deal this buff
    public EntityControllerOld Target;        // Who take this buff
    public int CurrentStack;        // How many stacks

    public BaseBuffItem(
        BuffDataSO buffData,
        EntityControllerOld caster,
        EntityControllerOld target,
        int curStack
    )
    {
        BuffData = buffData;
        Source = caster;
        Target = target;
        CurrentStack = curStack;
    }
    public int CompareTo(BaseBuffItem other)
    {
        if (other == null) return 1;
        return BuffData.Priority.CompareTo(other.BuffData.Priority);
    }
}

public enum BuffType
{
    Stackable,      // Can add stacks
    Independent,    // Isolated to invoke
}
public enum BuffStackType
{
    Extend,
    Refresh,
    None
}
public enum BuffRemoveType
{
    Reduce,
    Clear
}
#endregion