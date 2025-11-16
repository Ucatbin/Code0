using System.Linq;
using ThisGame.Core;
using ThisGame.Core.CheckerSystem;
using ThisGame.Entity.SkillSystem;
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

        void OnEnable()
        {
            EventBus.Subscribe<MoveButtonPressed>(this, HandleMovePressed);
            EventBus.Subscribe<MoveButtonRelease>(this, HandleMoveRelease);
        }
        void OnDisable()
        {
            EventBus.UnsubscribeAll(this);
        }
        
        protected override void Start()
        {
            base.Start();
            
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

        public void RequestStateChange<T>() where T : IState
        {
            _stateMachine.ChangeState<T>();
        }
        #region InputSystem
        Vector3 _inputValue;
        public Vector3 InputValue => _inputValue;
        public void HandleMovePressed(MoveButtonPressed @event)
        {
            _inputValue = @event.MoveDirection;
            if (_inputValue.x * FacingDir < 0)
            {
                _facingDir *= -1;
                var viewFlip = new ViewFlip
                {
                    FacingDir = _facingDir
                };
                EventBus.Publish(viewFlip);
            }
        }
        public void HandleMoveRelease(MoveButtonRelease @event)
        {
            _inputValue = Vector3.zero;
        }
        #endregion

        #region StateMachineSystem
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
            // Skills
            var attackSkill = GetController<SkillController>().GetSkill<P_AttackModel>();
            _stateMachine.RegisterState<P_AttackState>(
                new P_AttackState(this, _stateMachine, "Attack", checkerController, moveController.Model, attackSkill)
            );
            var grappingHookSkill = GetController<SkillController>().GetSkill<P_GrappingHookModel>();
            _stateMachine.RegisterState<P_HookedState>(
                new P_HookedState(this, _stateMachine, "Hooked", checkerController, moveController.Model, grappingHookSkill)
            );
        }
        #endregion
    }
}