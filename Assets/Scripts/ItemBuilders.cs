using System;
using UnityEngine;

#region DamageBuilder
/// <summary>
/// Damage Info
/// </summary>
[Serializable]
public struct DamageData
{
    public Transform Caster;
    public int DamageAmount;
    public Vector2 DamageDirection;
    public float KnockbackForce;
    public DamageType DamageType;

    public DamageData(
        Transform caster,
        int damageAmount,
        Vector2 damageDirection,
        float knockbackForce,
        DamageType damageType = DamageType.Normal
    )
    {
        Caster = caster;
        DamageAmount = damageAmount;
        DamageDirection = damageDirection;
        KnockbackForce = knockbackForce;
        DamageType = damageType;
    }
}

/// <summary>
/// 伤害类型枚举
/// </summary>
public enum DamageType
{
    Normal,
    Physics,     // 普通物理伤害
    Magic,      // 魔法伤害
    Fire,       // 火焰伤害
    Ice,        // 冰冻伤害
    Lightning,  // 雷电伤害
    Poison,     // 毒素伤害
    True        // 真实伤害（无视防御）
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