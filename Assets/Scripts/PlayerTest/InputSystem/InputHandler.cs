using ThisGame.Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ThisGame.Entity.InputSystem
{
    public class InputHandler : MonoBehaviour
    {
        public void HandleMove(InputAction.CallbackContext context)
        {
            Vector2 input = context.ReadValue<Vector2>();

            var movePressed = new MoveButtonPressed
            {
                MoveDirection = new Vector3(input.x, 0, input.y)
            };

            EventBus.Publish(movePressed);
        }

        public void HandleJump(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                var jumpPressedEvent = new JumpButtonPressed();
                EventBus.Publish(jumpPressedEvent);
            }
            if (context.canceled)
            {
                var jumpReleaseEvent = new JumpButtonReleased();
                EventBus.Publish(jumpReleaseEvent);
            }
        }
    }
}