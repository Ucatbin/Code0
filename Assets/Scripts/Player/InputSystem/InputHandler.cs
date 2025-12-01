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

                var (model, entry) = _player.GetController<SkillController>().GetSkill<P_DoubleJumpModel>();
                var skillPressed = new P_SkillPressed()
                {
                    Skill = model
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
            var (attackModel, attackEntry) =  _player.GetController<SkillController>().GetSkill<P_AttackModel>();
            var (dashAttackModel, dashAttackEntry) = _player.GetController<SkillController>().GetSkill<P_DashAttackModel>();
            if (context.performed)
            {
                var attackPressed = new P_SkillPressed()
                {
                    Skill =  attackModel,
                    InputDirection = _player.MainCam.ScreenToWorldPoint(Input.mousePosition),
                };
                EventBus.Publish(attackPressed);

                var dashAttackExecute = new P_SkillExecute()
                {
                    Skill = dashAttackModel,
                    InputDirection = _player.MainCam.ScreenToWorldPoint(Input.mousePosition),
                    StartTime = Time.time,
                    PlayerPosition = _player.transform.position
                };
                EventBus.Publish(dashAttackExecute);
            }
        }
        #endregion
        #region Skills
        public void HandleGrappingHook(InputAction.CallbackContext context)
        {
            var (model, entity) =  _player.GetController<SkillController>().GetSkill<P_GrappingHookModel>();
            if (context.performed)
            {
                var skillPressed = new P_SkillPressed()
                {
                    Skill =  model,
                    InputDirection = _player.MainCam.ScreenToWorldPoint(Input.mousePosition),
                    PlayerPosition = _player.transform.position
                };
                EventBus.Publish(skillPressed);
            }
            if (context.canceled)
            {
                var skillRelease = new P_SkillReleased()
                {
                    Skill = model,
                };
                EventBus.Publish(skillRelease);
            }
        }
        public void HandleRopeDash(InputAction.CallbackContext context)
        {
            if (context.performed)
                EventBus.Publish(new P_Skill_RopeDashPressed());
        }
        public void HandleDashAttack(InputAction.CallbackContext context)
        {
            var (model, entity) =  _player.GetController<SkillController>().GetSkill<P_DashAttackModel>();
            if (context.performed)
            {
                var skillPressed = new P_SkillPressed()
                {
                    Skill =  model,
                };
                EventBus.Publish(skillPressed);
            }
            if (context.canceled)
            {
                var skillRelease = new P_SkillReleased()
                {
                    Skill = model,
                };
                EventBus.Publish(skillRelease);
            }
        }
        #endregion
    }
}