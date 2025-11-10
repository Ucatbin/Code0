using ThisGame.Core;
using ThisGame.Core.CheckerSystem;
using ThisGame.Entity.EntitySystem;
using ThisGame.Entity.SkillSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ThisGame.Entity.InputSystem
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] PlayerController _player;

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

                var doubleJumpSkill = _player.GetController<SkillController>().GetSkill<P_DoubleJumpModel>("P_DoubleJump");
                var doubleJumpPressedEvent = new P_Skill_DoubleJumpPressed()
                {
                    Skill = doubleJumpSkill
                };
                EventBus.Publish(doubleJumpPressedEvent);
            }
            if (context.canceled)
            {
                var jumpReleaseEvent = new JumpButtonReleased();
                EventBus.Publish(jumpReleaseEvent);
            }
        }

        public void HandleGrappingHook(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                var grappingHookPressedEvent = new P_Skill_GrappingHookPressed()
                {
                    Skill = _player.GetController<SkillController>().GetSkill<P_GrappingHookModel>("P_GrappingHook"),
                    CurrentPosition = _player.transform.position,
                    InputDirection = _player.MainCam.ScreenToWorldPoint(Input.mousePosition),
                    IsGrounded = _player.GetController<CheckerController>().GetChecker<GroundCheckModel>("GroundCheckModel").IsDetected
                };
                EventBus.Publish(grappingHookPressedEvent);
            }
            if (context.canceled)
            {
                var grappingHookRelease = new P_Skill_GrappingHookReleased()
                {
                    Skill = _player.GetController<SkillController>().GetSkill<P_GrappingHookModel>("P_GrappingHook"),
                };
                EventBus.Publish(grappingHookRelease);
            }
        }
    }
}