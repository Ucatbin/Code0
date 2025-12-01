using System;
using ThisGame.Core;
using ThisGame.Core.CheckerSystem;
using ThisGame.Entity.EntitySystem;
using ThisGame.Entity.MoveSystem;
using ThisGame.Entity.SkillSystem;
using UnityEngine;

namespace ThisGame.Entity.StateMachineSystem
{
    public class P_DashAttackState : P_BaseState
    {
        P_DashAttackModel _skill;
        P_DashAttackData _data;
        P_DashAttackView _view;
        
        public P_DashAttackState(
            PlayerController entity,
            StateMachine stateMachine,
            string animName,
            CheckerController checkers,
            MoveModel movement,
            P_DashAttackModel skill,
            SkillEntry entry
        ) : base(entity, stateMachine, animName, checkers, movement)
        {
            _skill = skill;
            _data = entry.Data as P_DashAttackData;
            _view = entry.View as P_DashAttackView;
        }

        protected override Type[] AcceptedEvents => new Type[]
        {
            typeof(P_SkillStateSwitch),
            typeof(P_SkillExecute),
            typeof(P_SkillReleased),
        };
        protected override Type[] AcceptedSkillPressEvents => new Type[]
        {
            typeof(P_DashAttackModel)
        };
        protected override Type[] AcceptedSkillExecuteEvents => new Type[]
        {
            typeof(P_DashAttackModel)  
        };

        public override void Enter()
        {
            base.Enter();
            
            SmoothTime.SetSmoothTimeScale(_data.SlowTimeScale);
            _view.StartAiming();
            _skill.IsDashing = false;
        }
        
        public override void Exit()
        {
            base.Exit();

            SmoothTime.SetSmoothTimeScale(_data.NormalTimeScale);
            _view.StopAiming();
            _skill.IsDashing = false;
        }
        
        public override void LogicUpdate()
        {
            _view.UpdateView();
            if (!_skill.IsDashing)
            {
                _movement.UpdateMovement(Vector3.zero, Time.deltaTime);
                _player.Rb.linearVelocity = _movement.Velocity;
            }
            else
                UpdateDashMovement();

            if (Input.GetMouseButtonDown(0))
            {
                _player.View.Animator.SetTrigger("DashAttackDash");   
                _view.StopAiming();  
                var attackDir = (_skill.DashTargetPos - _player.transform.position).normalized;
                if (attackDir.x * _player.FacingDir < 0)
                {
                    var viewFlip = new ViewFlip()
                    {
                        FacingDir = -_player.FacingDir
                    };
                    EventBus.Publish(viewFlip);
                }
            }
        }
        
        public override void PhysicsUpdate()
        {
            if (!_skill.IsDashing)
            {
                if (_player.Rb.linearVelocityY >= 0)
                    _movement.HandleGravity(Time.fixedDeltaTime);
                else
                    _movement.HandleGravity(SmoothTime.FixedDeltaTime);
            }
        }

        private void UpdateDashMovement()
        {
            float elapsedTime = Time.time - _skill.DashStartTime;
            float progress = Mathf.Clamp01(elapsedTime / _skill.DashDuration);

            float easedProgress = 1 - (1 - progress) * (1 - progress);
            
            Vector3 newPosition = Vector3.Lerp(_skill.DashStartPos, _skill.DashTargetPos, easedProgress);
            _player.transform.parent.position = newPosition;
            
            if (progress >= 1f)
                EndDash();
        }

        void EndDash()
        {
            _skill.IsDashing = false;
            _stateMachine.ChangeState<P_AirState>();
        }
    }
}