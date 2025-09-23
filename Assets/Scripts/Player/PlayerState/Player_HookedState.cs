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
        _gHookSkill = Player_SkillManager.Instance.GrappingHook;
        _dashSkill = Player_SkillManager.Instance.GrappingHookDash;

        _player.Rb.gravityScale = _player.PropertySO.FallGravity;
        _player.RTProperty.TargetSpeed = Vector2.zero;
    }

    public override void PhysicsUpdate()
    {
        _dashSkill.TryUseSkill();
        _gHookSkill.MoveOnLine();
    }
    public override void LogicUpdate()
    {
        _gHookSkill.CheckLineBreak();

        _gHookSkill.RopeLine.SetPosition(0, _player.transform.position);
    }

    public override void Exit()
    {
        base.Exit();
    }
}