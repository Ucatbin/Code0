using System.Collections;
using UnityEngine;

public class Player_HookedState : Player_BaseState
{
    PlayerSkill_GrappingHook _gHookSkill;
    PlayerSkill_GrappingHookDash _dashSkill;

    public Player_HookedState(PlayerController_Main entity, StateMachine stateMachine, int priority, string stateName) : base(entity, stateMachine, priority, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _gHookSkill = Player_SkillManager.Instance.GrappingHook;
        _dashSkill = Player_SkillManager.Instance.GrappingHookDash;
    }

    public override void PhysicsUpdate()
    {
        if (!_gHookSkill.IsHookFinished)
            return;

        _dashSkill.TryUseSkill();
        _gHookSkill.MoveOnLine();
    }
    public override void LogicUpdate()
    {
        _gHookSkill.CheckLineBreak();
        
        if (!_gHookSkill.IsHookFinished)
            return;

        _player.ApplyMovement();

        _gHookSkill.SetLineRenderer();
    }

    public override void Exit()
    {
        base.Exit();

        _player.Rb.gravityScale = 0;
        _player.IsBusy = false;
    }
}