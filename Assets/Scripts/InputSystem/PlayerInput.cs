using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] PlayerController_Main _player;
    // Public Input
    public Vector2 MouseDir { get; private set; }
    public Vector2 MoveInput { get; private set; }
    public bool JumpTrigger { get; private set; }
    public bool GrapperTrigger { get; private set; }
    public bool DashTrigger { get; private set; }
    public bool AttackTrigger { get; private set; }

    public void HandleMove(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
    }

    public void HandleJump(InputAction.CallbackContext context)
    {
        JumpTrigger = context.performed;
    }

    public void HandleGrappingHook(InputAction.CallbackContext context)
    {
        Vector2 mousePos = _player.MainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = (mousePos - (Vector2)_player.transform.position).normalized;
        MouseDir = dir;
        if (context.canceled) MouseDir = Vector2.zero;
        GrapperTrigger = context.performed;
    }

    public void HandleSprint(InputAction.CallbackContext context)
    {
        DashTrigger = context.performed;
    }

    public void HandleAttack(InputAction.CallbackContext context)
    {
        Vector2 mousePos = _player.MainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = (mousePos - (Vector2)_player.transform.position).normalized;
        MouseDir = dir;
        AttackTrigger = context.performed;

        if (context.canceled) MouseDir = Vector2.zero;
    }
}