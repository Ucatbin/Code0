using System.Linq;
using ThisGame.Core;
using ThisGame.Core.CheckerSystem;
using ThisGame.Entity.StateMachineSystem;
using UnityEngine;

namespace ThisGame.Entity.EntitySystem
{
    public class PlayerController : EntityController
    {
        public Camera MainCam;
        public DistanceJoint2D Joint;

        public PlayerView View;
        StateMachine _stateMachine;
        public BaseController[] Controllers;
        void OnEnable()
        {
            EventBus.Subscribe<MoveButtonPressed>(this, HandleMovePressed);
            EventBus.Subscribe<MoveButtonRelease>(this, HandleMoveRelease);
            EventBus.Subscribe<StateChange>(this, View.HandleStateChange);
        }
        void OnDisable()
        {
            EventBus.UnsubscribeAll(this);
        }
        
        protected override void Start()
        {
            foreach (var controller in Controllers)
                controller.Initialize();

            RegisterStates();
            _stateMachine.Initialize<P_IdleState>();
        }
        
        protected override void Update()
        {
            _stateMachine.CurrentState.LogicUpdate();
        }
        protected override void FixedUpdate()
        {
            _stateMachine.CurrentState.PhysicsUpdate();
        }

        Vector3 _inputValue;
        public Vector3 InputValue => _inputValue;
        public void HandleMovePressed(MoveButtonPressed e)
        {
            _inputValue = e.MoveDirection;
            if (_inputValue.x * FacingDir < 0)
            {
                _facingDir *= -1;
                View.FlipSprite(FacingDir);
            }
        }
        public void HandleMoveRelease(MoveButtonRelease e)
        {
            _inputValue = Vector3.zero;
        }

        public T GetController<T>() where T : BaseController
        {
            return Controllers.OfType<T>().FirstOrDefault();
        }
        void RegisterStates()
        {
            _stateMachine = new StateMachine();
            var checkerController = GetController<CheckerController>();
            var moveController = GetController<MoveController>();

            _stateMachine.RegisterState<P_AirState>(
                new P_AirState(this, _stateMachine, "Air", checkerController, moveController.Model)
            );
            _stateMachine.RegisterState<P_IdleState>(
                new P_IdleState(this, _stateMachine, "Idle", checkerController, moveController.Model)
            );
            _stateMachine.RegisterState<P_MoveState>(
                new P_MoveState(this, _stateMachine, "Move", checkerController, moveController.Model)
            );
            _stateMachine.RegisterState<P_JumpState>(
                new P_JumpState(this, _stateMachine, "Jump", checkerController, moveController.Model)
            );
            _stateMachine.RegisterState<P_WallSlideState>(
                new P_WallSlideState(this, _stateMachine, "WallSlide", checkerController, moveController.Model)
            );
            _stateMachine.RegisterState<P_CoyotState>(
                new P_CoyotState(this, _stateMachine, "Air", checkerController, moveController.Model)
            );
            _stateMachine.RegisterState<P_HookedState>(
                new P_HookedState(this, _stateMachine, "Hooked", checkerController, moveController.Model)
            );
        }
    }
}