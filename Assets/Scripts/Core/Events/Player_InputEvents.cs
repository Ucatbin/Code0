using System;

public static class PlayerInputEvents
{
    // Grapping hook
    public static event Action OnGHookPressed;
    public static event Action OnGHookReleased;
    public static void TriggerGHookPressed() => OnGHookPressed?.Invoke();
    public static void TriggerGHookReleased() => OnGHookReleased?.Invoke();

    // Line Dash
    public static event Action OnLineDashPressed;
    public static event Action OnLineDashReleased;
    public static void TriggerLineDashPressed() => OnLineDashPressed?.Invoke();
    public static void TriggerLineDashReleased() => OnLineDashReleased?.Invoke();
}
