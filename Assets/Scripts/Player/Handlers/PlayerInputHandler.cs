using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [SerializeField] PlayerController_Main _player;

    [SerializeField] InputActionAsset InputActions;

    InputActionMap _normalActionMap;

    public Vector2 MouseDir { get; private set; }
    public Vector2 MoveInput { get; private set; }

    void Awake()
    {
        _normalActionMap = InputActions.FindActionMap("Normal");
        ChangeActionMap(_normalActionMap);
    }
    public void ChangeActionMap(InputActionMap nextMap)
    {
        InputActions.Disable();
        nextMap.Enable();
    }

    #region Normal Map
    public void HandleMove(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
    }

    public void HandleJump(InputAction.CallbackContext context)
    {
        if (context.performed && !_player.IsHooked)
            InputEvents.TriggerJumpPressed();
        if (context.canceled && !_player.IsHooked)
            InputEvents.TriggerJumpReleased();
    }

    public void HandleGHook(InputAction.CallbackContext context)
    {
        Vector2 mousePos = _player.MainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = (mousePos - (Vector2)_player.transform.position).normalized;
        MouseDir = dir;
        if (context.performed)
            InputEvents.TriggerGHookPressed();
        if (context.canceled)
        {
            InputEvents.TriggerGHookReleased();
            MouseDir = Vector2.zero;
        }
    }
    public void HandleAttack(InputAction.CallbackContext context)
    {
        Vector2 mousePos = _player.MainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = (mousePos - (Vector2)_player.transform.position).normalized;
        MouseDir = dir;
        if (context.performed)
            InputEvents.TriggerAttackPressed();
        if (context.canceled)
        {
            InputEvents.TriggerAttackReleased();
            MouseDir = Vector2.zero;
        }
    }
    public void HandleLineDash(InputAction.CallbackContext context)
    {
        if (context.performed && _player.IsHooked)
            InputEvents.TriggerLineDashPressed();
        if (context.canceled && _player.IsHooked)
            InputEvents.TriggerLineDashReleased();
    }
    #endregion
}

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

    // Dash
    public static event Action OnLineDashPressed;
    public static event Action OnLineDashReleased;
    public static void TriggerLineDashPressed() => OnLineDashPressed?.Invoke();
    public static void TriggerLineDashReleased() => OnLineDashReleased?.Invoke();
}