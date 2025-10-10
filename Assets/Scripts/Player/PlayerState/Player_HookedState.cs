using System.Collections;
using UnityEngine;

public class Player_HookedState : Player_BaseState
{
    // Necessary Component
    PlayerSkill_GrappingHook _gHookSkill;
    PlayerSkill_GrappingHookDash _dashSkill;

    public Player_HookedState(PlayerController_Main entity, StateMachine stateMachine, int priority, string stateName) : base(entity, stateMachine, priority, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        // Initialize grappling hook component
        _player.IsBusy = true;
        _gHookSkill = Player_SkillManager.Instance.GrappingHook;
        _dashSkill = Player_SkillManager.Instance.GrappingHookDash;

        _player.Rb.gravityScale = _player.PropertySO.DefaultGravity;
        _player.SetTargetVelocity(Vector2.zero);
    }

    public override void PhysicsUpdate()
    {
        _dashSkill.TryUseSkill();
        _gHookSkill.MoveOnLine();
    }
    public override void LogicUpdate()
    {
        _player.ApplyMovement();
        _gHookSkill.CheckLineBreak();

        _gHookSkill.RopeLine.SetPosition(0, _player.transform.position);
        _gHookSkill.RopeLine.SetPosition(1, _gHookSkill.HookPoint.transform.position);
    }

    public override void Exit()
    {
        base.Exit();

        _player.Rb.gravityScale = 0;
        _player.IsBusy = false;
    }
}