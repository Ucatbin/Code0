using System;
using ThisGame.Core.CheckerSystem;
using ThisGame.Entity.EntitySystem;
using ThisGame.Entity.HealthSystem;
using ThisGame.Entity.MoveSystem;
using UnityEngine;

namespace ThisGame.Entity.StateMachineSystem
{
    public class P_BeHitState : P_BaseState
    {
        public P_BeHitState(PlayerController entity, StateMachine stateMachine, string animName, CheckerController checkers, MoveModel movement) : base(entity, stateMachine, animName, checkers, movement)
        {
        }
        
        public override void Enter()
        {
            base.Enter();

            _player.GetController<HealthController>().Model.CanHit = false;
            _player.View.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            TimerManager.Instance.AddTimer(
                0.22f,
                () =>{
                    _stateMachine.ChangeState<P_IdleState>();
                    _player.View.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                }
            );
        }

        public override void Exit()
        {
            _player.GetController<HealthController>().Model.CanHit = true;
        }

        public override void LogicUpdate()
        {
            
        }

        public override void PhysicsUpdate()
        {
            _movement.HandleGravity(SmoothTime.FixedDeltaTime);
            _player.Rb.linearVelocity = _movement.Velocity;
        }
    }
}
