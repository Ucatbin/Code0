using System.Collections;
using UnityEngine;

public class Player_HookedState : Player_BaseState
{
    // Necessary Component
    PlayerSkill_GrappingHook _gHookSkill;
    PlayerSkill_GrappingHookDash _dashSkill;
    PlayerController_Checker _checker;

    public Player_HookedState(PlayerController_Main entity, StateMachine stateMachine, int priority, string stateName) : base(entity, stateMachine, priority, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        // Initialize grappling hook component
        _gHookSkill = Player_SkillManager.Instance.GrappingHook;
        _dashSkill = Player_SkillManager.Instance.GrappingHookDash;
        _checker = _player.Checker;

        _player.Rb.gravityScale = _player.PropertySO.FallGravity;
        _player.RTProperty.TargetSpeed = Vector2.zero;
    }

    public override void PhysicsUpdate()
    {
        _dashSkill.TryUseSkill();
        _gHookSkill.MoveOnGLine();
    }
    public override void LogicUpdate()
    {
        CheckGLineBreak();

        _gHookSkill.RopeLine.SetPosition(0, _player.transform.position);
    }

    public override void Exit()
    {
        base.Exit();

        _player.RTProperty.TargetSpeed = _player.Rb.linearVelocity;
    }

    void CheckGLineBreak()
    {
        if (!_player.InputSys.GrapperTrigger && _player.IsAttached)
        {
            _gHookSkill.ReleaseGHook();
            return;
        }
        if (_checker.GLineChecker.IsTouchingLayers(_checker.GLineBreakLayer))
        {
            _gHookSkill.BreakGHook();
            return;
        }

        // RaycastHit2D[] hits = new RaycastHit2D[2];
        // int hitCount = Physics2D.RaycastNonAlloc(
        //     _player.transform.position,
        //     (_gHookSkill.HookPoint.transform.position - _player.transform.position).normalized,
        //     hits,
        //     _gHookSkill.RopeJoint.distance,
        //     _gHookSkill.CanHookLayer
        // );
        // if (hitCount > 1)
        //     _gHookSkill.BreakGHook();
    }
}