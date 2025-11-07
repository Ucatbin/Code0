using ThisGame.Entity.EntitySystem;
using UnityEngine;

namespace ThisGame.Entity.StateMachineSystem
{
    public class P_BaseState : BaseState
    {
        protected PlayerController _player;
        public P_BaseState(PlayerController entity, StateMachine stateMachine) : base(entity, stateMachine)
        {
            _player = entity;
        }

        public override void Enter()
        {
            
        }

        public override void Exit()
        {
            
        }

        public override void LogicUpdate()
        {
            
        }

        public override void PhysicsUpdate()
        {
            
        }
    }

}
