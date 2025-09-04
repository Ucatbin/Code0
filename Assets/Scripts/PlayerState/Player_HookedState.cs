using System.Collections;
using UnityEngine;

public class Player_HookedState : Player_BaseState
{
    // Necessary Component
    PlayerSkill_GrappingHook _grappingHook;
    PlayerChecker _checker;
    bool _sprint;
    PlayerSkill_GrappingHookDash _dashSkill;
    bool _shouldAddForce;

    public Player_HookedState(PlayerController entity, StateMachine stateMachine, string stateName) : base(entity, stateMachine, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        // Initialize grappling hook component
        _grappingHook = Player_SkillManager.Instance.GrappingHook;
        _checker = _player.Checker;
        _dashSkill = Player_SkillManager.Instance.GrappingHookDash;

        // Enable player collision check
        _checker.GLineChecker.enabled = true;

        _player.Rb.gravityScale = _player.AttributeSO.MaxFallGravity;
    }

    public override void PhysicsUpdate()
    {
        _sprint = _player.InputSys.DashTrigger;
        // Check if the grapping line is broken
        CheckGLineBreak();
        // Control the length of the grapping line
        _dashSkill.BasicCheck();
        if (!_sprint)
            Player_SkillManager.Instance.GrappingHook.MoveOnGLine();


        if (_shouldAddForce)
            _player.Rb.AddForce(new Vector2(
                _player.InputSys.MoveInput.x * _grappingHook.LineSwingForce,
                0f
            ), ForceMode2D.Force);
    }
    public override void LogicUpdate()
    {
        /* Release hook when button is released and currently attached or
        when grapping line is broken*/
        if ((!_player.InputSys.GrapperTrigger && _player.IsAttached) ||
            _checker.GLineChecker.IsTouchingLayers(_checker.GLineBreakLayer)
        )
            _grappingHook.ReleaseHook();

        // If current velocity less than max speed, can add force
        _shouldAddForce = Mathf.Abs(_player.Rb.linearVelocity.magnitude) < _grappingHook.MaxSwingSpeed;

        // Update line renderer position
        _grappingHook.RopeLine.SetPosition(0, _player.transform.position);
    }

    public override void Exit()
    {
        base.Exit();
        
        // Disable player collision check
        _checker.GLineChecker.enabled = false;

        Player_SkillManager.Instance.GrappingHook.CoolDownSkill();
    }

    void CheckGLineBreak()
    {
        if (_grappingHook.RopeJoint.distance > _grappingHook.MaxDetectDist)
        {
            _grappingHook.ReleaseHook();
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
            _grappingHook.ReleaseHook();
    }
}