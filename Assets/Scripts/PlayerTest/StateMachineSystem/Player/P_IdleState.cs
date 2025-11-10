using System;
using ThisGame.Core.CheckerSystem;
using ThisGame.Entity.EntitySystem;
using ThisGame.Entity.MoveSystem;

namespace ThisGame.Entity.StateMachineSystem
{
    public class P_IdleState : P_GroundState
    {
        public P_IdleState(PlayerController entity, StateMachine stateMachine, CheckerController checkers, MoveModel movement) : base(entity, stateMachine, checkers, movement)
        {
        }
        protected override Type[] GetInputEvents() => new Type[]
        {
            typeof(JumpButtonPressed),
            typeof(MoveButtonPressed)
        };
        protected override Type[] GetSkillEvents() => new Type[]
        {
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