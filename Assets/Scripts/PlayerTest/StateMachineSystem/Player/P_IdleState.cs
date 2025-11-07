using ThisGame.Entity.EntitySystem;
using ThisGame.Entity.MoveSystem;
using UnityEngine;

namespace ThisGame.Entity.StateMachineSystem
{
    public class P_IdleState : P_GroundState
    {
        public P_IdleState(PlayerController entity, StateMachine stateMachine, MoveModel model) : base(entity, stateMachine, model)
        {
        }

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

            if (_movement.IsMoving)
                _stateMachine.ChangeState("Move");
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}