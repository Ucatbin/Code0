using System;
using UnityEngine;

public static class BuffEvents
{
    public static event Action<float, BaseBuffItem> OnBuffUpdate;
    public static void TriggerBuffUpdate(float timer, BaseBuffItem buffItem) => OnBuffUpdate?.Invoke(timer, buffItem);
}