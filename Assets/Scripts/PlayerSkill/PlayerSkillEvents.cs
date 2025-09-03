using System;

public static class GrappleEvent
{
    public static event Action OnHookAttached;
    public static event Action OnHookReleased;
    public static void TriggerHookAttached() => OnHookAttached?.Invoke();
    public static void TriggerHookReleased() => OnHookReleased?.Invoke();

}