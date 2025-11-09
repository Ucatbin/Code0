using System;
using ThisGame.Core.CheckerSystem;
using ThisGame.Entity.SkillSystem;
using ThisGame.Entity.EntitySystem;
using ThisGame.Entity.MoveSystem;
using ThisGame.Core;

namespace ThisGame.Entity.StateMachineSystem
{
    public class P_MoveState : P_GroundState
    {
        public P_MoveState(PlayerController entity, StateMachine stateMachine, CheckerController checkers, MoveModel movement) : base(entity, stateMachine, checkers, movement)
        {
        }

        protected override Type[] SubscribeEvents => new Type[]
        {
            // Movement
            typeof(JumpButtonPressed),
            // Skills
            typeof(P_Skill_GrappingHookPressed),
            typeof(P_Skill_GrappingHookPrepare)
        };

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            var groundCheck = _checkers.GetChecker<GroundCheckModel>("GroundCheckModel");
            if (!groundCheck.IsDetected)
                _stateMachine.ChangeState("Air");        
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        void HandleMoveReleased(MoveButtonReleased e)
        {
            _stateMachine.ChangeState("Idle");
        }
        void HandleGrappingHookPressed(P_Skill_GrappingHookPressed e)
        {
            e.Skill.HandleSkillButtonPressed(e);
        }
        void HandleGrappingHookPrepare(P_Skill_GrappingHookPrepare e)
        {
            _stateMachine.ChangeState("Hooked");
            var grappingHookExecute = new P_Skill_GrappingHookExecuted()
            {
                Skill = _player.GetController<SkillController>().GetSkill<P_GrappingHookModel>("P_GrappingHook"),
                IsGrounded = true,
                TargetPosition = e.TargetPosition
            };
            EventBus.Publish(grappingHookExecute);
        }
    }
}