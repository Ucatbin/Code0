using UnityEngine;

public class Player_AttackState : Player_BaseState
{
    public Player_AttackState(PlayerController entity, StateMachine stateMachine, string stateName) : base(entity, stateMachine, stateName)
    {
    }
    public override void Enter()
    {
        base.Enter();

        Player_TimerManager.Instance.AddTimer(
            Player_SkillManager.Instance.Attack.AttackDuration,
            () => { SkillEvents.TriggerAttackEnd(); },
            "Player_AbilityTimer"
        );

        _player.AttributeSO.TargetVelocity = Vector2.zero;
        _player.Rb.gravityScale = _player.AttributeSO.AttackGravity;

        Vector2 mousePos = _player.MainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 _dir = (mousePos - (Vector2)_player.transform.position).normalized;
        _player.AttributeSO.TargetVelocity = _dir * Player_SkillManager.Instance.Attack.AttackForce;
    }
    public override void PhysicsUpdate()
    {
        _player.Rb.linearVelocity = _player.AttributeSO.TargetVelocity;
    }
    public override void LogicUpdate()
    {

    }
    public override void Exit()
    {
        base.Exit();

        _player.AttributeSO.TargetVelocity = Vector2.zero;
        Player_SkillManager.Instance.Attack.CoolDownSkill();
        _player.Rb.gravityScale = _player.AttributeSO.MaxFallGravity;
    }
}
