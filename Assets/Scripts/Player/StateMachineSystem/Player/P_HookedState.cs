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
        P_GrapplingHookView _view;
        bool _isInited => _skill.Joint.distance <= _data.MaxLineDist;

        public P_HookedState(
            PlayerController entity,
            StateMachine stateMachine,
            string animName,
            CheckerController checkers,
            MoveModel movement,
            P_GrappingHookModel skill,
            SkillEntry entry
        ) : base(entity, stateMachine, animName, checkers, movement)
        {
            _skill = skill;
            _data = entry.Data as P_GrappingHookData;
            _view = entry.View as P_GrapplingHookView;
        }

        protected override Type[] AcceptedEvents => new Type[]
        {
            typeof(P_SkillStateSwitch),
            typeof(P_SkillReleased),
        };
        protected override Type[] AcceptedSkillPressEvents => new Type[]
        {
            typeof(P_GrappingHookModel)
        };

        public override void Enter()
        {
            base.Enter();
            _checkers.GetChecker<GHookCheckModel>().EnableChecker(true);
            if ((_skill.HookPoint.transform.position.x - _player.transform.position.x) * _player.FacingDir < 0)
            {
                var viewFlip = new ViewFlip()
                {
                    FacingDir = -_player.FacingDir
                };
                EventBus.Publish(viewFlip);
            }

            _player.Rb.gravityScale = 4f;
            var length = Vector2.Distance(_player.transform.position, _skill.HookPoint.transform.position);
            _skill.EnableJoint(length);
            _view?.EnableRope(_player.transform, _skill.HookPoint.transform);
        }
        public override void Exit()
        {
            base.Exit();

            _view?.DisableRope();
            _movement.SetVelocity(_player.Rb.linearVelocity);
            _player.Rb.gravityScale = 0f;
        }

        public override void LogicUpdate()
        {
            _player.View.Animator.SetFloat("HookedDir", _player.FacingDir * _player.Rb.linearVelocityX);
            var ghookGroundCheck = _checkers.GetChecker<GHookCheckModel>();
            // ========== 双射线地面检测 ==========
            Vector2 playerPos = _player.transform.position;
            
            // 1. 当前位置向下检测
            RaycastHit2D currentGroundHit = Physics2D.Raycast(
                playerPos, 
                Vector2.down, 
                _data.GroundDetectAhead, 
                _data.GroundLayerMask
            );
        
            // 2. 移动方向前方检测（预判）
            Vector2 moveDirection = new Vector2(_player.FacingDir, 0);
            
            RaycastHit2D aheadGroundHit = Physics2D.Raycast(
                playerPos + moveDirection * 0.5f, 
                Vector2.down, 
                _data.GroundDetectAhead, 
                _data.GroundLayerMask
            );
        
            // ========== 取较高的地面 ==========
            float highestGroundY = float.MinValue;
            bool hasGround = false;
            
            if (currentGroundHit.collider != null)
            {
                highestGroundY = currentGroundHit.point.y;
                hasGround = true;
            }
            if (aheadGroundHit.collider != null && aheadGroundHit.point.y > highestGroundY)
            {
                highestGroundY = aheadGroundHit.point.y;
                hasGround = true;
            }
        
            // ========== 计算并缩短绳长 ==========
            if (hasGround)
            {
                // 计算玩家应该保持的最小高度
                float minPlayerY = highestGroundY +_data.MinGroundClearance;
                
                // 当玩家接近地面时触发缩绳
                if (playerPos.y < minPlayerY + 0.3f)
                {
                    // 计算钩爪点到玩家的向量
                    Vector2 toHook = (Vector2)_skill.HookPoint.transform.position - playerPos;
                    
                    // 计算保持最小高度所需的绳长
                    float dx = toHook.x;
                    float dy = _skill.HookPoint.transform.position.y - minPlayerY;
                    
                    if (dy > 0) // 钩爪点必须在目标高度上方
                    {
                        // 勾股定理计算所需绳长
                        float requiredLength = Mathf.Sqrt(dx * dx + dy * dy);
                        requiredLength = Mathf.Max(requiredLength, _data.MinRopeLength);
                        
                        // 平滑缩短绳长
                        if (requiredLength < _skill.Joint.distance)
                        {
                            _skill.Joint.distance = Mathf.MoveTowards(
                                _skill.Joint.distance,
                                requiredLength,
                                _data.RopeShortenSpeed * Time.deltaTime
                            );
                        }
                    }
                    
                    // 硬约束：确保玩家绝不低于最小高度
                    if (playerPos.y < minPlayerY)
                    {
                        // 直接调整玩家位置到安全高度
                        _player.transform.parent.position = new Vector3(playerPos.x, minPlayerY, 0);
                        
                        // 重新计算绳长
                        float newDistance = Vector2.Distance(
                            _player.transform.parent.position, 
                            _skill.HookPoint.transform.position
                        );
                        _skill.Joint.distance = Mathf.Min(newDistance, _skill.Joint.distance);
                    }
                }
            }
        }
        public override void PhysicsUpdate()
        {
            if (_isInited)
                _skill.ControlRope(_player.InputValue, _player.Rb, _skill.Joint, Time.fixedDeltaTime);
            else
            {
                _skill.Joint.distance = Mathf.MoveTowards(
                    _skill.Joint.distance,
                    _data.MaxLineDist,
                    10f
                );
            }
        }
    }
}