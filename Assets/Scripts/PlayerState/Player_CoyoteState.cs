public class Player_CoyoteState : Player_FallState
{
    public Player_CoyoteState(PlayerController_Main entity, StateMachine stateMachine, int priority, string stateName) : base(entity, stateMachine, priority, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        TimerManager.Instance.AddTimer(
            _player.PlayerItem.Property.CoyoteWindow,
            () => _stateMachine.ChangeState(_player.PlayerItem.State.FallState, false),
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
            _stateMachine.ChangeState(_player.PlayerItem.State.JumpState, true);
    }

    public override void Exit()
    {
        base.Exit();

        TimerManager.Instance.CancelTimersWithTag("Coyote");
    }

}
