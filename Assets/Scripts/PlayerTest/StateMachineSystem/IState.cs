using UnityEngine;

namespace ThisGame.Entity.StateMachineSystem
{
    public interface IState
    {
        void Enter();
        void PhysicsUpdate();
        void LogicUpdate();
        void Exit();
    }
}