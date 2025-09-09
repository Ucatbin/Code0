using System;

public static class SkillEvents
{
    public static event Action OnJumpStart;
    public static event Action OnJumpEnd;
    public static void TriggerJumpStart() => OnJumpStart?.Invoke();
    public static void TriggerJumpEnd() => OnJumpEnd?.Invoke();
    
    public static event Action OnHookAttach;
    public static event Action OnHookRelease;
    public static void TriggerHookAttach() => OnHookAttach?.Invoke();
    public static void TriggerHookReleas() => OnHookRelease?.Invoke();

    public static event Action OnAttackStart;
    public static event Action OnAttackEnd;
    public static void TriggerAttackStart() => OnAttackStart?.Invoke();
    public static void TriggerAttackEnd() => OnAttackEnd?.Invoke();

}