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
        
        // 冲刺相关变量
        private float dashStartTime;
        private bool isDashing = false;
        
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
            isDashing = false;
        }
        
        public override void Exit()
        {
            base.Exit();

            SmoothTime.SetSmoothTimeScale(_data.NormalTimeScale);
            _view.StopAiming();
            isDashing = false;
        }
        
        public override void LogicUpdate()
        {
            _view.UpdateView();
            if (!isDashing)
            {
                _movement.UpdateMovement(Vector3.zero, Time.deltaTime);
                _player.Rb.linearVelocity = _movement.Velocity;
            }
            else
                UpdateDashMovement();

            if (Input.GetMouseButtonDown(0))
            {
                dashStartTime = Time.time;
            
                isDashing = true;
                
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
            if (!isDashing)
            {
                if (_player.Rb.linearVelocityY >= 0)
                    _movement.HandleGravity(Time.fixedDeltaTime);
                else
                    _movement.HandleGravity(SmoothTime.FixedDeltaTime);
            }
        }

        private void UpdateDashMovement()
        {
            float elapsedTime = Time.time - dashStartTime;
            float progress = Mathf.Clamp01(elapsedTime / _skill.DashDuration);
            
            // 使用缓动函数让移动更平滑
            float easedProgress = EaseOutQuad(progress);
            
            // 计算当前位置
            Vector3 newPosition = Vector3.Lerp(_skill.DashStartPos, _skill.DashTargetPos, easedProgress);
            
            // 直接设置玩家位置
            _player.transform.parent.position = newPosition;
            
            // // 更新刚体位置（如果需要物理交互）
            // _player.Rb.position = newPosition;
            
            // 检查冲刺是否完成
            if (progress >= 1f)
            {
                EndDash();
            }
        }

        private void EndDash()
        {
            isDashing = false;
            _stateMachine.ChangeState<P_AirState>();
        }

        // 缓动函数 - 二次方缓出
        private float EaseOutQuad(float t)
        {
            return 1 - (1 - t) * (1 - t);
        }
    }
}