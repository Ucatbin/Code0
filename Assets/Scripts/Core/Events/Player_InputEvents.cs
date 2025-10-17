using System;

public static class InputEvents
{
    // Jump
    public static event Action OnJumpPressed;
    public static event Action OnJumpReleased;
    public static void TriggerJumpPressed() => OnJumpPressed?.Invoke();
    public static void TriggerJumpReleased() => OnJumpReleased?.Invoke();

    // Attack
    public static event Action OnAttackPressed;
    public static event Action OnAttackReleased;
    public static void TriggerAttackPressed() => OnAttackPressed?.Invoke();
    public static void TriggerAttackReleased() => OnAttackReleased?.Invoke();

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
