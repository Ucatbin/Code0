using System;
using UnityEngine;

#region EntityBuilder
public class PlayerRuntimeProperty
{
    // Base property
    public float BaseMaxSpeedGround { get; set; }
    public float BaseMaxSpeedAir { get; set; }
    public float BaseAttackDamage { get; set; }
    
    // Buff modifier
    public float MaxSpeedGroundMultiplier { get; set; } = 1f;
    public float MaxSpeedAirMultiplier { get; set; } = 1f;
    public float AttackDamageMultiplier { get; set; } = 1f;
    
    public float MaxSpeedGroundBonus { get; set; } = 0f;
    public float MaxSpeedAirBonus { get; set; } = 0f;
    public float FlatAttackDamageBonus { get; set; } = 0f;
    
    // Final property
    public float FMaxMoveSpeedGround => (BaseMaxSpeedGround + MaxSpeedGroundBonus) * MaxSpeedGroundMultiplier;
    public float FMaxSpeedAir => (BaseMaxSpeedAir + MaxSpeedAirBonus) * MaxSpeedAirMultiplier;
    public float FinalAttackDamage => (BaseAttackDamage + FlatAttackDamageBonus) * AttackDamageMultiplier;
}
public class EntityItem
{
    public Vector2 TargetSpeed;
}
public class PlayerItem : EntityItem
{
    public PlayerPropertySO Property;
    public PlayerStateSO State;
    public PlayerRuntimeProperty RuntimeProperty;
    public float MaxAirVelocityX;
    public float MaxGroundVelocityX;

    public PlayerItem(PlayerPropertySO property, PlayerStateSO state)
    {
        Property = property;
        State = state;
    }
}
#endregion

#region BuffBuilder
[Serializable]
public class BuffItem : IComparable<BuffItem>
{
    public BuffDataSO BuffData; // Get the data of buffSO
    public GameObject Source;   // Who deal this buff
    public GameObject Target;   // Who take this buff
    public int CurrentStack;    // How many stacks

    public BuffItem(
        BuffDataSO buffData,
        GameObject caster,
        GameObject target,
        int curStack
    )
    {
        BuffData = buffData;
        Source = caster;
        Target = target;
        CurrentStack = curStack;
    }
    public int CompareTo(BuffItem other)
    {
        if (other == null) return 1;
        return BuffData.Priority.CompareTo(other.BuffData.Priority);
    }
}

public enum BuffType
{
    Unique,
    Stackable,
    Independent
}
public enum BuffStackType
{
    ExtendDuration,
    RefreshDuration,
    None
}
public enum BuffRemoveType
{
    Reduce,
    Clear
}
#endregion

#region TimerBuilder
public class TimerItem : IComparable<TimerItem>
{
    public float TriggerTime;   // Absolute trigger time on the timeline
    public Action Callback;     // Action to callback when the timer triggers
    public bool IsLoop;         // Is it a looping timer
    public float Interval;      // Interval for looping timers
    public object Tag;          // Tag for identification
    public float RemainingTime; // Remaining time when paused
    public bool IsPaused;       // Is the timer paused

    public TimerItem(
        float triggerTime,
        Action callback,
        bool isLoop = false,
        float interval = 0f,
        object tag = null
    )
    {
        TriggerTime = triggerTime;
        Callback = callback;
        IsLoop = isLoop;
        Interval = interval;
        Tag = tag;
    }
    public int CompareTo(TimerItem other)
    {
        if (other == null) return 1;
        return TriggerTime.CompareTo(other.TriggerTime);
    }
}
#endregion