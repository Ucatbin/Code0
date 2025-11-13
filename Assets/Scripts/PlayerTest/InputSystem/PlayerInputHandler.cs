using ThisGame.Core;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandlerOld : MonoBehaviour
{
    [SerializeField] InputActionAsset _inputActions;
    [SerializeField] Camera _mainCam;
    InputActionMap _normalActionMap;

    public Vector2 MouseDir { get; private set; }
    public Vector2 MoveInput { get; private set; }

    void Awake()
    {
        _normalActionMap = _inputActions.FindActionMap("Normal");
        ChangeActionMap(_normalActionMap);
    }
    public void ChangeActionMap(InputActionMap nextMap)
    {
        _inputActions?.Disable();
        nextMap.Enable();
    }

    #region Normal Map
    // Abilities
    public void HandleMove(InputAction.CallbackContext ctx)
    {
        Vector2 input = ctx.ReadValue<Vector2>();
        
        var moveEvent = new MoveButtonPressed { 
            MoveDirection = new Vector3(input.x, 0, input.y)
        };

        EventBus.Publish(moveEvent);
    }

    public void HandleJump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            InputEvents.TriggerJumpPressed();
        if (ctx.canceled)
            InputEvents.TriggerJumpReleased();
    }

    // Skills
    public void HandleGHook(InputAction.CallbackContext ctx)
    {
        Vector2 mousePos = _mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = (mousePos - (Vector2)transform.position).normalized;
        MouseDir = dir;
        if (ctx.performed)
            InputEvents.TriggerGHookPressed();
        if (ctx.canceled)
        {
            InputEvents.TriggerGHookReleased();
            MouseDir = Vector2.zero;
        }
    }
    public void HandleAttack(InputAction.CallbackContext ctx)
    {
        Vector2 mousePos = _mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = (mousePos - (Vector2)transform.position).normalized;
        MouseDir = dir;
        if (ctx.performed)
            InputEvents.TriggerAttackPressed();
        if (ctx.canceled)
        {
            InputEvents.TriggerAttackReleased();
            MouseDir = Vector2.zero;
        }
    }
    public void HandleLineDash(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            InputEvents.TriggerLineDashPressed();
        if (ctx.canceled)
            InputEvents.TriggerLineDashReleased();
    }
    #endregion
}