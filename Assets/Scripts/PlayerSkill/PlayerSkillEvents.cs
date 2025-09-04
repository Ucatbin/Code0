using System;

public static class SkillEvents
{
    public static event Action OnHookAttached;
    public static event Action OnHookReleased;
    public static void TriggerHookAttached() => OnHookAttached?.Invoke();
    public static void TriggerHookReleased() => OnHookReleased?.Invoke();

    public static event Action OnAttacking;
    public static void TriggerAttack() => OnAttacking?.Invoke();

}