public class Player_GroundState : Player_BaseState
{
    public Player_GroundState(PlayerController_Main entity, StateMachine stateMachine, int priority, string stateName) : base(entity, stateMachine, priority, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
    
    public override void PhysicsUpdate()
    {

    }
    public override void LogicUpdate()
    {
        if (!_player.Checker.IsGrounded)
            _stateMachine.ChangeState(_player.StateSO.CoyoteState, false);
    }

    public override void Exit()
    {
        base.Exit();
    }
}