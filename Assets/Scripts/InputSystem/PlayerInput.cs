using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerInput : MonoBehaviour
{
    // Public Input
    public Vector2 MoveInput { get; private set; }
    public bool JumpTrigger { get; private set; }
    public bool GrapperTrigger { get; private set; }
    public bool SprintTrigger { get; private set; }
    public bool AttackTrigger { get; private set; }

    public void HandleMove(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
    }

    public void HandleJump(InputAction.CallbackContext context)
    {
        JumpTrigger = context.performed;
    }

    public void HandleGrappingHook(InputAction.CallbackContext contex)
    {
        GrapperTrigger = contex.performed;
    }

    public void HandleSprint(InputAction.CallbackContext context)
    {
        SprintTrigger = context.performed;
    }

    public void HandleAttack(InputAction.CallbackContext context)
    {
        AttackTrigger = context.performed;
    }
}