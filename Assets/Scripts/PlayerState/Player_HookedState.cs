using System.Collections;
using UnityEngine;

public class Player_HookedState : Player_BaseState
{
    // Necessary Component
    PlayerSkill_GrappingHook _grappingHook;
    PlayerSkill_GrappingHookDash _dashSkill;
    PlayerChecker _checker;
    bool _shouldAddForce;
    bool _shouldMove;
    bool _shouldCheck;
    public Player_HookedState(PlayerController entity, StateMachine stateMachine, string stateName) : base(entity, stateMachine, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        // Initialize grappling hook component
        _grappingHook = Player_SkillManager.Instance.GrappingHook;
        _dashSkill = Player_SkillManager.Instance.GrappingHookDash;
        _checker = _player.Checker;
        _shouldCheck = false;

        _player.Rb.gravityScale = _player.AttributeSO.MaxFallGravity;
    }

    public override void PhysicsUpdate()
    {
        // Check if the grapping line is broken
        CheckGLineBreak();

        _dashSkill.BasicSkillCheck();
        if (_shouldMove)
            _grappingHook.MoveOnGLine();

        // if (_shouldCheck)
        //     GroundMove();
    }
    public override void LogicUpdate()
    {
        /* Release hook when button is released and currently attached or
            when grapping line is broken*/
            if ((!_player.InputSys.GrapperTrigger && _player.IsAttached) ||
                _checker.GLineChecker.IsTouchingLayers(_checker.GLineBreakLayer)
            )
                _grappingHook.ReleaseGHook();

        // Update line renderer position
        _grappingHook.RopeLine.SetPosition(0, _player.transform.position);
    }

    public override void Exit()
    {
        base.Exit();

        _grappingHook.CoolDownSkill();
        _player.AttributeSO.TargetVelocity = _player.Rb.linearVelocity;
    }

    void CheckGLineBreak()
    {
        if (_grappingHook.RopeJoint.distance > _grappingHook.MaxDetectDist)
        {
            _grappingHook.ReleaseGHook();
            return;
        }

        RaycastHit2D[] hits = new RaycastHit2D[2];
        int hitCount = Physics2D.RaycastNonAlloc(
            _player.transform.position,
            (_grappingHook.HookPoint.transform.position - _player.transform.position).normalized,
            hits,
            _grappingHook.RopeJoint.distance,
            _grappingHook.CanHookLayer
        );
        if (hitCount > 1)
            _grappingHook.ReleaseGHook();
    }
}