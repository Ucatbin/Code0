using UnityEngine;

public class Player_AirState : Player_BaseState
{
    public Player_AirState(PlayerController_Main entity, StateMachine stateMachine, int priority, string stateName) : base(entity, stateMachine, priority, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();


        if (Player_SkillManager.Instance.Jump.FinishJump)
            if (!_player.InputSys.JumpTrigger)
                Player_SkillManager.Instance.Jump.IsReady = true;
    }
    public override void LogicUpdate()
    {
        // Reset IsJumping to enable ground check, enter fallState
        if (_player.Rb.linearVelocityY < 0f && _stateMachine.CurrentState != _player.StateSO.FallState)
            _stateMachine.ChangeState(_player.StateSO.FallState, false);

        // Exit when detect the ground
        if (_player.Checker.IsGrounded && _stateMachine.CurrentState != _player.StateSO.JumpState)
            _stateMachine.ChangeState(_player.StateSO.IdleState, true);
    }

    public override void Exit()
    {
        base.Exit();
    }
}