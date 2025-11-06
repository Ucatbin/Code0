using UnityEngine;

public class Player_FallState : Player_AirState
{
    public Player_FallState(PlayerController_Main entity, StateMachineOld stateMachine, int priority, string stateName) : base(entity, stateMachine, priority, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_player.CheckerSys.IsWallDected && _player.InputSys.MoveInput.x == _player.FacingDir)
            _stateMachine.ChangeState(_player.StateSO.WallSlideState, false);
    }

    public override void Exit()
    {
        base.Exit();
    }
}