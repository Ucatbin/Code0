using System;

public static class SkillEvents
{
    public static event Action OnHookAttached;
    public static event Action OnHookReleased;
    public static void TriggerHookAttached() => OnHookAttached?.Invoke();
    public static void TriggerHookReleased() => OnHookReleased?.Invoke();

    public static event Action OnAttackStart;
    public static event Action OnAttackEnd;
    public static void TriggerAttack() => OnAttackStart?.Invoke();
    public static void TriggerAttackEnd() => OnAttackEnd?.Invoke();

}