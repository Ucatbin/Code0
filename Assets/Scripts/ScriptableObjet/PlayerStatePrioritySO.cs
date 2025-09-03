using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "StatePriorityConfig", menuName = "Game/Player/StatePriority")]
public class PlayerStatePrioritySO : EntityStatePriorityCfgSO
{
    [Header("Movement States Priority")]
    [Min(0)] public int IdlePriority = 1;
    [Min(0)] public int MovePriority = 1;
    [Min(0)] public int AirPriority = 1;
    [Min(0)] public int JumpPriority = 2;
    [Min(0)] public int FallPriority = 1;
    [Min(0)] public int AirGlidePriority = 2;

    [Header("Action States Priority")]
    [Min(0)] public int HookedPriority = 4;
    [Min(0)] public int AttackPriority = 3;
    [Min(0)] public int DashPriority = 3;
    [Min(0)] public int UltimatePriority = 5;

    private Dictionary<Type, int> _priorityCache;

    private void OnEnable()
    {
        InitializeCache();
    }

    private void InitializeCache()
    {
        _priorityCache = new Dictionary<Type, int>
        {
            { typeof(Player_IdleState), IdlePriority },
            { typeof(Player_MoveState), MovePriority },
            { typeof(Player_AirState), AirPriority },
            { typeof(Player_JumpState), JumpPriority },
            { typeof(Player_FallState), FallPriority },
            { typeof(Player_HookedState), HookedPriority },
            { typeof(Player_AirGlideState), AirGlidePriority }
        };
    }

    public override int GetPriority(Type stateType)
    {
        if (_priorityCache.TryGetValue(stateType, out int priority))
            return priority;
        return 1;
    }

    private void OnValidate()
    {
        if (_priorityCache != null)
        {
            UpdateCache();
        }
    }

    private void UpdateCache()
    {
        _priorityCache[typeof(Player_IdleState)] = IdlePriority;
        _priorityCache[typeof(Player_MoveState)] = MovePriority;
        _priorityCache[typeof(Player_AirState)] = AirPriority;
        _priorityCache[typeof(Player_JumpState)] = JumpPriority;
        _priorityCache[typeof(Player_FallState)] = FallPriority;
        _priorityCache[typeof(Player_HookedState)] = HookedPriority;
        _priorityCache[typeof(Player_AirGlideState)] = AirGlidePriority;
    }
}
