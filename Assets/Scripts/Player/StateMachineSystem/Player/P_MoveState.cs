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
        public P_MoveState(PlayerController entity, StateMachine stateMachine, string animName, CheckerController checkers, MoveModel movement) : base(entity, stateMachine, animName, checkers, movement)
        {
        }

        protected override Type[] AcceptedEvents => new Type[]
        {
            //
            typeof(BeHit),
            // Abilities
            typeof(JumpButtonPressed),
            typeof(JumpExecute),
            // Skills
            typeof(P_SkillPressed),
            typeof(P_SkillStateSwitch),
        };
        protected override Type[] AcceptedSkillPressEvents => new Type[]
        {
            typeof(P_AttackModel),
            typeof(P_GrappingHookModel),
            typeof(P_DashAttackModel)
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
                _stateMachine.ChangeState<P_CoyotState>();

            if (_player.InputValue.x == 0)
                _stateMachine.ChangeState<P_IdleState>();   
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}