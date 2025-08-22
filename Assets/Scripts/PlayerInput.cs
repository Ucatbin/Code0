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

    public void HandleMove(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
    }

    public void HandleJump(InputAction.CallbackContext context)
    {
        if (context.performed)
            JumpTrigger = true;
        else if (context.canceled)
            JumpTrigger = false;
    }
}