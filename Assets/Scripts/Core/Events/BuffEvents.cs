using System;
using UnityEngine;

public static class BuffEvents
{
    public static event Action<float, BaseBuffModel> OnBuffUpdate;
    public static void TriggerBuffUpdate(float timer, BaseBuffModel buffItem) => OnBuffUpdate?.Invoke(timer, buffItem);
}