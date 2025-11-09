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
        public Camera MainCam;
        public DistanceJoint2D Joint;

        public StateMachine StateMachine;
        public BaseController[] Controllers;
        void OnEnable()
        {
            EventBus.Subscribe<MoveButtonPressed>(this, OnMovePressed);
        }
        void OnDisable()
        {
            EventBus.Unsubscribe<MoveButtonPressed>(OnMovePressed);
        }
        
        protected override void Start()
        {
            foreach (var controller in Controllers)
                controller.Initialize();

            RegisterStates();
            StateMachine.Initialize("Idle");
        }
        
        protected override void Update()
        {
            StateMachine.CurrentState.LogicUpdate();
        }
        protected override void FixedUpdate()
        {
            StateMachine.CurrentState.PhysicsUpdate();
        }

        Vector3 _inputValue;
        public Vector3 InputValue => _inputValue;
        public void OnMovePressed(MoveButtonPressed moveInputInfo)
        {
            _inputValue = moveInputInfo.MoveDirection;
        }

        public T GetController<T>() where T : BaseController
        {
            return Controllers.OfType<T>().FirstOrDefault();
        }
        void RegisterStates()
        {
            StateMachine = new StateMachine();
            var checkerController = GetController<CheckerController>();
            var moveController = GetController<MoveController>();

            StateMachine.RegisterState(
                "Ground",
                new P_GroundState(this, StateMachine, checkerController, moveController.Model)
            );
            StateMachine.RegisterState(
                "Air",
                new P_AirState(this, StateMachine, checkerController, moveController.Model)
            );
            StateMachine.RegisterState(
                "Idle",
                new P_IdleState(this, StateMachine, checkerController, moveController.Model)
            );
            StateMachine.RegisterState(
                "Move",
                new P_MoveState(this, StateMachine, checkerController, moveController.Model)
            );
            StateMachine.RegisterState(
                "Jump",
                new P_JumpState(this, StateMachine, checkerController, moveController.Model)
            );
            StateMachine.RegisterState(
                "WallSlide",
                new P_WallSlideState(this, StateMachine, checkerController, moveController.Model)
            );
            StateMachine.RegisterState(
                "Coyot",
                new P_CoyotState(this, StateMachine, checkerController, moveController.Model)
            );
            StateMachine.RegisterState(
                "Hooked",
                new P_HookedState(this, StateMachine, checkerController, moveController.Model)
            );
        }
    }
}