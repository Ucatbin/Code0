using ThisGame.Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ThisGame.InputSystem
{
    public class InputHandler : MonoBehaviour
    {
        public void HandleMove(InputAction.CallbackContext context)
        {
            Vector2 input = context.ReadValue<Vector2>();

            var moveEvent = new PlayerMoveInputEvent
            {
                MoveDirection = new Vector3(input.x, 0, input.y)
            };

            EventBus.Publish(moveEvent);
        }
    }
}