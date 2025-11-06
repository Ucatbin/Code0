using ThisGame.Entity.StateMachineSystem;
using UnityEngine;

namespace ThisGame.Entity.EntitySystem
{
    public class PlayerController : EntityController
    {
        public StateMachine StateMachine;
        [SerializeField] MoveController _movement;
        protected override void Start()
        {
            RegisterStates();
        }
        
        void RegisterStates()
        {
            StateMachine = new StateMachine();

            StateMachine.RegisterState("Idle", new P_IdleState(this, _movement.Model));
        }
    }
}