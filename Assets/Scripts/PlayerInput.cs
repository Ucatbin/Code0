using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] Player _player;

    // Public Input
    public Vector2 MoveInput { get; private set; }
    public bool JumpTrigger { get; private set; }
    public bool GrapperTrigger { get; private set; }
    public Vector2 ScrollInput { get; private set; }

    public void HandleMove(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
    }

    public void HandleJump(InputAction.CallbackContext context)
    {
        JumpTrigger = context.performed;
    }

    public void HandleGrapper(InputAction.CallbackContext contex)
    {
        GrapperTrigger = contex.performed;
    }

    public void HandleScroll(InputAction.CallbackContext context)
    {
        ScrollInput = context.ReadValue<Vector2>();
    }
}