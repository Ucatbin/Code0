using ThisGame.Entity.EntitySystem;
using ThisGame.Entity.MoveSystem;
using UnityEngine;

namespace ThisGame.Entity.StateMachineSystem
{
    public class P_JumpState : P_AirState
    {
        public P_JumpState(PlayerController entity, StateMachine stateMachine, MoveModel model) : base(entity, stateMachine, model)
        {
        }

        public override void Enter()
        {
            base.Enter();

            _movement.SetVelocity(new Vector3(0f, _movement.Data.BaseJumpSpeed, 0f));
            _player.Rb.linearVelocity = _movement.Velocity;
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}
