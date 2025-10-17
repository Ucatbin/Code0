public class Player_CoyoteState : Player_FallState
{
    public Player_CoyoteState(PlayerController_Main entity, StateMachine stateMachine, int priority, string stateName) : base(entity, stateMachine, priority, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        TimerManager.Instance.AddTimer(
            Player_SkillManager.Instance.Jump.CoyoteWindow,
            () =>
            {
                _stateMachine.ChangeState(_player.StateSO.FallState, false);
                Player_SkillManager.Instance.Jump.CurrentCharges -= 1;
            },
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
    }

    public override void Exit()
    {
        base.Exit();

        TimerManager.Instance.CancelTimersWithTag("Coyote");
    }

}
