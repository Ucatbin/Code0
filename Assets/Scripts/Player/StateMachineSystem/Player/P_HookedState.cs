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
        P_GrappingHookData _data;

        public P_HookedState(PlayerController entity, StateMachine stateMachine, string animName, CheckerController checkers, MoveModel movement, P_GrappingHookModel skill) : base(entity, stateMachine, animName, checkers, movement)
        {
            _skill = skill;
            _data = _skill.Data as P_GrappingHookData;
        }

        protected override Type[] GetEvents() => new Type[]
        {
            // Skills
            typeof(P_Skill_GrappingHookReleased)
        };

        public override void Enter()
        {
            base.Enter();

            _player.Rb.gravityScale = 4f;
            _player.Joint.connectedBody = _skill.HookPoint.GetComponent<Rigidbody2D>();
            _player.Joint.distance = Vector2.Distance(_player.transform.position, _skill.HookPoint.transform.position);
            _player.Joint.enabled = true;
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
    }
}