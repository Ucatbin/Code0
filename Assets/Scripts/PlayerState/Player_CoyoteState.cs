public class Player_CoyoteState : Player_FallState
{
    public Player_CoyoteState(PlayerController_Main entity, StateMachine stateMachine, int priority, string stateName) : base(entity, stateMachine, priority, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Player_TimerManager.Instance.AddTimer(
            _player.AttributeSO.CoyoteWindow,
            () => _stateMachine.ChangeState(_player.StateSO.FallState, false),
            "Coyote"
        );
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_player.InputSys.JumpTrigger)
            _stateMachine.ChangeState(_player.StateSO.JumpState, true);
    }

    public override void Exit()
    {
        base.Exit();

        Player_TimerManager.Instance.CancelTimersWithTag("Coyote");
    }

}
