using System;
using ThisGame.Core.CheckerSystem;
using ThisGame.Entity.EntitySystem;
using ThisGame.Entity.MoveSystem;

namespace ThisGame.Entity.StateMachineSystem
{
    public class P_IdleState : P_GroundState
    {
        public P_IdleState(PlayerController entity, StateMachine stateMachine, string animName, CheckerController checkers, MoveModel movement) : base(entity, stateMachine, animName, checkers, movement)
        {
        }

        protected override Type[] GetEvents() => new Type[]
        {
            // Ability
            typeof(JumpButtonPressed),
            // Skills
            typeof(P_Skill_AttackPressed),
            typeof(P_Skill_AttackExecute),
            typeof(P_Skill_GrappingHookPressed),
            typeof(P_Skill_GrappingHookExecute),
            typeof(P_Skill_DashAttackPressed),
            typeof(P_Skill_DashAttackExecuted)
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
                _stateMachine.ChangeState<P_AirState>();

            if (_player.InputValue.x != 0 && _player.Rb.linearVelocityX != 0)
                _stateMachine.ChangeState<P_MoveState>();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}