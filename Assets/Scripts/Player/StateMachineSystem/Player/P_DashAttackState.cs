using System;
using ThisGame.Core.CheckerSystem;
using ThisGame.Entity.EntitySystem;
using ThisGame.Entity.MoveSystem;
using ThisGame.Entity.SkillSystem;
using ThisGame.Entity.StateMachineSystem;
using UnityEngine;

namespace ThisGame.Entity.StateMachineSystem
{
    public class P_DashAttackState : P_BaseState
    {
        P_DashAttackModel _skill;
        P_DashAttackData _data;
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
            _data = _skill.Data as P_DashAttackData;
        }

        protected override Type[] GetEvents() => new Type[]
        {
            typeof(P_Skill_TheWorldRelease),
        };

        public override void Enter()
        {
            base.Enter();
            
            Time.timeScale = _data.SlowTimeScale;
        }
        public override void Exit()
        {
            base.Exit();

            Time.timeScale = _data.NormalTimeScale;
        }
        public override void LogicUpdate()
        {
            _movement.UpdateMovement(Vector3.zero, Time.deltaTime * 100);
        }
        public override void PhysicsUpdate()
        {
            _movement.HandleGravity(Time.fixedDeltaTime * 100);

            if (Input.GetMouseButtonDown(0))
            {
                Time.timeScale = 1f;
                _player.View.Animator.SetTrigger("DashAttackDash");
            }
        }
    }
}