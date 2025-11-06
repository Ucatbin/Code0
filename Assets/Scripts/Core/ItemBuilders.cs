using System;
using System.Linq;
using UnityEngine;

#region DamageBuilder
/// <summary>
/// Damage Info
/// </summary>
[Serializable]
public struct DamageData
{
    public EntityControllerOld Caster;
    public int DamageAmount;
    public Vector2 DamageDirection;
    public float KnockbackForce;
    public DamageType DamageType;

    public DamageData(
        EntityControllerOld caster,
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

    public void HandleHit()
    {
        var buffsToProcess = Caster.BuffSys.BuffHeap.ToList();
        foreach (var buffInfo in buffsToProcess)
            buffInfo?.BuffData.OnHit?.Apply(buffInfo);
    }
    public void HandleKill()
    {
        var buffsToProcess = Caster.BuffSys.BuffHeap.ToList();
        foreach (var buffInfo in buffsToProcess)
            buffInfo?.BuffData.OnKill?.Apply(buffInfo);
    }
}
#endregion

#region TimerBuilder
[Serializable]
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