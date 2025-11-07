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
    public void HandleMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        
        var moveEvent = new MoveButtonPressed { 
            MoveDirection = new Vector3(input.x, 0, input.y)
        };

        EventBus.Publish(moveEvent);
    }

    public void HandleJump(InputAction.CallbackContext context)
    {
        if (context.performed)
            InputEvents.TriggerJumpPressed();
        if (context.canceled)
            InputEvents.TriggerJumpReleased();
    }

    public void HandleGHook(InputAction.CallbackContext context)
    {
        Vector2 mousePos = _mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = (mousePos - (Vector2)transform.position).normalized;
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
        Vector2 mousePos = _mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = (mousePos - (Vector2)transform.position).normalized;
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
        if (context.performed)
            InputEvents.TriggerLineDashPressed();
        if (context.canceled)
            InputEvents.TriggerLineDashReleased();
    }
    #endregion
}