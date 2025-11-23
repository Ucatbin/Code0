using System;
using ThisGame.Core;
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
        bool _isInited => _initLength <= _data.MaxLineDist;
        float _initLength;

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

            if ((_skill.HookPoint.transform.position.x - _player.transform.position.x) * _player.FacingDir < 0)
            {
                var viewFlip = new ViewFlip()
                {
                    FacingDir = -_player.FacingDir
                };
                EventBus.Publish(viewFlip);
            }
            _player.Rb.gravityScale = 4f;
            _player.Joint.connectedBody = _skill.HookPoint.GetComponent<Rigidbody2D>();
            _player.Joint.distance = Vector2.Distance(_player.transform.position, _skill.HookPoint.transform.position);
            _player.Joint.enabled = true;
            _initLength = _player.Joint.distance;
        }
        public override void Exit()
        {
            base.Exit();

            _movement.SetVelocity(_player.Rb.linearVelocity);
            _player.Rb.gravityScale = 0f;
        }

        public override void LogicUpdate()
        {
            _player.View.Animator.SetFloat("HookedDir", _player.FacingDir * _player.Rb.linearVelocityX);
        }
        public override void PhysicsUpdate()
        {
            if (_isInited)
                _skill.ControlRope(_player.InputValue, _player.Rb, _player.Joint, Time.fixedDeltaTime);
            else
            {
                _player.Joint.distance = Mathf.MoveTowards(
                    _initLength,
                    _data.MaxLineDist,
                    10f
                );
                _initLength = _player.Joint.distance;
            }
        }
    }
}