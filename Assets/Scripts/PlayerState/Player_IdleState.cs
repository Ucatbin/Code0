using UnityEngine;

public class Player_IdleState : Player_GroundState
{
    public Player_IdleState(PlayerController_Main entity, StateMachine stateMachine, int priority, string stateName) : base(entity, stateMachine, priority, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _player.PlayerItem.TargetSpeed.y = 0f;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        _player.PlayerItem.TargetSpeed.x = Mathf.MoveTowards(
            _player.PlayerItem.TargetSpeed.x,
            0,
            _player.PlayerItem.Property.GroundDamping * Time.fixedDeltaTime
        );

        _player.Rb.linearVelocity = new Vector2(
            _player.PlayerItem.TargetSpeed.x,
            _player.Rb.linearVelocity.y
        );
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // Change to moveState when have InputX and is not holding jump
        if (_player.InputSys.MoveInput.x != 0f)
            _stateMachine.ChangeState(_player.PlayerItem.State.MoveState, false);
    }

    public override void Exit()
    {
        base.Exit();
    }
}