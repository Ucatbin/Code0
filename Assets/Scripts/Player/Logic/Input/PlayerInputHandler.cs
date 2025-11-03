using ThisGame.Events.AbilityEvents;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [SerializeField] InputActionAsset _inputActions;
    [SerializeField] Camera _mainCam;
    InputActionMap _normalActionMap;
    IEventBus _eventBus;

    public Vector2 MouseDir { get; private set; }
    public Vector2 MoveInput { get; private set; }

    void Awake()
    {
        _normalActionMap = _inputActions.FindActionMap("Normal");
        ChangeActionMap(_normalActionMap);
    }
    void Start()
    {
        _eventBus = ServiceLocator.Get<IEventBus>();
    }
    public void ChangeActionMap(InputActionMap nextMap)
    {
        _inputActions.Disable();
        nextMap.Enable();
    }

    #region Normal Map
    public void HandleMove(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
    }

    public void HandleJump(InputAction.CallbackContext context)
    {
        if (context.performed)
            _eventBus.Publish(new AbilityInputTriggerPressed("Jump"));
        if (context.canceled)
            _eventBus.Publish(new AbilityInputTriggerReleased("Jump"));
    }

    public void HandleGHook(InputAction.CallbackContext context)
    {
        Vector2 mousePos = _mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = (mousePos - (Vector2)transform.position).normalized;
        MouseDir = dir;
        if (context.performed)
            PlayerInputEvents.TriggerGHookPressed();
        if (context.canceled)
        {
            PlayerInputEvents.TriggerGHookReleased();
            MouseDir = Vector2.zero;
        }
    }
    public void HandleAttack(InputAction.CallbackContext context)
    {
        Vector2 mousePos = _mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = (mousePos - (Vector2)transform.position).normalized;
        MouseDir = dir;
        if (context.performed)
            _eventBus.Publish(new AbilityInputTriggerPressed("Attack"));
        if (context.canceled)
        {
            _eventBus.Publish(new AbilityInputTriggerReleased("Attack"));
            MouseDir = Vector2.zero;
        }
    }
    public void HandleLineDash(InputAction.CallbackContext context)
    {
        if (context.performed)
            PlayerInputEvents.TriggerLineDashPressed();
        if (context.canceled)
            PlayerInputEvents.TriggerLineDashReleased();
    }
    #endregion
}