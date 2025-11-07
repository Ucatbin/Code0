using System.Linq;
using ThisGame.Core;
using ThisGame.Core.CheckerSystem;
using ThisGame.Entity.StateMachineSystem;
using UnityEngine;

namespace ThisGame.Entity.EntitySystem
{
    public class PlayerController : EntityController
    {
        public Rigidbody2D Rb;

        public StateMachine StateMachine;
        public BaseController[] Controllers;

        protected override void Start()
        {
            foreach (var controller in Controllers)
                controller.Initialize();

            RegisterStates();
            StateMachine.Initialize("Idle");
        }
        
        public T GetController<T>() where T : BaseController
        {
            return Controllers.OfType<T>().FirstOrDefault();
        }
        void RegisterStates()
        {
            StateMachine = new StateMachine();

            StateMachine.RegisterState(
                "Ground",
                new P_GroundState(this, StateMachine, GetController<MoveController>().Model)
            );
            StateMachine.RegisterState(
                "Air",
                new P_AirState(this, StateMachine, GetController<MoveController>().Model)
            );
            StateMachine.RegisterState(
                "Idle",
                new P_IdleState(this, StateMachine, GetController<MoveController>().Model)
            );
            StateMachine.RegisterState(
                "Move",
                new P_MoveState(this, StateMachine, GetController<MoveController>().Model)
            );
            StateMachine.RegisterState(
                "Jump",
                new P_JumpState(this, StateMachine, GetController<MoveController>().Model)
            );
        }

        protected override void Update()
        {
            StateMachine.CurrentState.LogicUpdate();
        }
        protected override void FixedUpdate()
        {
            StateMachine.CurrentState.PhysicsUpdate();
        }
    }
}