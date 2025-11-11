using System;
using ThisGame.Core.CheckerSystem;
using ThisGame.Entity.EntitySystem;
using ThisGame.Entity.MoveSystem;
using ThisGame.Entity.SkillSystem;
using UnityEngine;

namespace ThisGame.Entity.StateMachineSystem
{
    public class P_HookedState : P_BaseState
    {
        P_GrappingHookModel _skill;

        public P_HookedState(PlayerController entity, StateMachine stateMachine, string animName, CheckerController checkers, MoveModel movement) : base(entity, stateMachine, animName, checkers, movement)
        {
        }

        protected override Type[] GetEvents() => new Type[]
        {
            // Skills
            typeof(P_Skill_GrappingHookExecuted),
            typeof(P_Skill_GrappingHookReleased)
        };

        public override void Enter()
        {
            base.Enter();
            _player.Rb.gravityScale = 4f;
        }
        public override void Exit()
        {
            base.Exit();
            _movement.SetVelocity(_player.Rb.linearVelocity);
            _player.Rb.gravityScale = 0f;
        }

        public override void LogicUpdate()
        {

        }
        public override void PhysicsUpdate()
        {
            _skill.ControlRope(_player.InputValue, _player.Rb, _player.Joint, Time.fixedDeltaTime);
        }
        protected override void HandleGrappingHookExecute(P_Skill_GrappingHookExecuted @event)
        {
            _skill = @event.Skill;
            _player.Joint.connectedBody = _skill.HookPoint.GetComponent<Rigidbody2D>();
            _player.Joint.distance = Vector2.Distance(_player.transform.position, @event.Skill.HookPoint.transform.position);
            _player.Joint.enabled = true;
        }
    }
}