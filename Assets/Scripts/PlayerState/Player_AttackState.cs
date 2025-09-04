using UnityEngine;

public class Player_AttackState : Player_BaseState
{
    Vector2 _dir;
    public Player_AttackState(PlayerController entity, StateMachine stateMachine, string stateName) : base(entity, stateMachine, stateName)
    {
    }
    public override void Enter()
    {
        base.Enter();

        _player.IsAttacking = true;
        _player.Rb.gravityScale = 0f;
        Player_TimerManager.Instance.AddTimer(
            Player_SkillManager.Instance.Attack.AttackDuration,
            () => { _stateMachine.ChangeState(_player.IdleState, true); },
            "Player_AbilityTimer"
        );

        Vector2 mousePos = _player.MainCam.ScreenToWorldPoint(Input.mousePosition);
        _dir = (mousePos- (Vector2)_player.transform.position).normalized;
    }
    public override void PhysicsUpdate()
    {
        _player.Rb.linearVelocity = _dir * Player_SkillManager.Instance.Attack.AttackMovement;
    }
    public override void LogicUpdate() { }
    public override void Exit()
    {
        base.Exit();
        
        _player.Rb.linearVelocity = Vector2.zero;
        Player_SkillManager.Instance.Attack.CoolDownSkill();
        _player.IsAttacking = false;
        _player.Rb.gravityScale = _player.AttributeSO.DefaultGravity;
    }
}
