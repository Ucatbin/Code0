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
        protected override Type[] GetEvents() => new Type[]
        {
            // Input
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

            var groundCheck = _checkers.GetChecker<GroundCheckModel>();
            if (!groundCheck.IsDetected)
                _stateMachine.ChangeState("Air");        
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}