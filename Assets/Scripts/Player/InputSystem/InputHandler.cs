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

        #region Abilities
        public void HandleMove(InputAction.CallbackContext context)
        {
            Vector2 input = context.ReadValue<Vector2>();

            if (context.performed)
            {
                var skillPressed = new MoveButtonPressed
                {
                    MoveDirection = new Vector3(input.x, 0, input.y)
                };
                EventBus.Publish(skillPressed);
            }
            if  (context.canceled)
            {
                EventBus.Publish(new MoveButtonRelease());
            }
        }

        public void HandleJump(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                var jumpPressedEvent = new JumpButtonPressed();
                EventBus.Publish(jumpPressedEvent);

                var doubleJumpSkill = _player.GetController<SkillController>().GetSkill<P_DoubleJumpModel>();
                var skillPressed = new P_Skill_DoubleJumpPressed()
                {
                    Skill = doubleJumpSkill
                };
                EventBus.Publish(skillPressed);
            }
            if (context.canceled)
            {
                var jumpReleaseEvent = new JumpButtonRelease();
                EventBus.Publish(jumpReleaseEvent);
            }
        }

        public void HandleAttack(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                var attackSkill =  _player.GetController<SkillController>().GetSkill<P_AttackModel>();
                var skillPressed = new P_Skill_AttackPressed()
                {
                    Skill =  attackSkill,
                    InputDirection = _player.MainCam.ScreenToWorldPoint(Input.mousePosition),
                };
                EventBus.Publish(skillPressed);
            }
        }
        #endregion
        #region Skills
        public void HandleGrappingHook(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                var skillPressed = new P_Skill_GrappingHookPressed()
                {
                    Skill = _player.GetController<SkillController>().GetSkill<P_GrappingHookModel>(),
                    CurrentPosition = _player.transform.position,
                    InputDirection = _player.MainCam.ScreenToWorldPoint(Input.mousePosition),
                    IsGrounded = _player.GetController<CheckerController>().GetChecker<GroundCheckModel>().IsDetected
                };
                EventBus.Publish(skillPressed);
            }
            if (context.canceled)
            {
                var grappingHookRelease = new P_Skill_GrappingHookRelease()
                {
                    Skill = _player.GetController<SkillController>().GetSkill<P_GrappingHookModel>(),
                };
                EventBus.Publish(grappingHookRelease);
            }
        }
        public void HandleTheWorld(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                var skillPressed = new P_Skill_DashAttackPressed()
                {
                    Skill = _player.GetController<SkillController>().GetSkill<P_DashAttackModel>(),
                };
                EventBus.Publish(skillPressed);
            }
            if (context.canceled)
            {
                var skillRelersed = new P_Skill_TheWorldRelease();
                EventBus.Publish(skillRelersed);
            }
        }
        #endregion
    }
}