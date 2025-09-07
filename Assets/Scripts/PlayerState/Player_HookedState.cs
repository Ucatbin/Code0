using System.Collections;
using UnityEngine;

public class Player_HookedState : Player_BaseState
{
    // Necessary Component
    PlayerSkill_GrappingHook _grappingHook;
    PlayerSkill_GrappingHookDash _dashSkill;
    PlayerChecker _checker;
    bool _shouldAddForce;
    float _realDist;
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
        if (!_player.Checker.IsGrounded)
            _grappingHook.SetJoint();
        _grappingHook.ApplyAttachForce();

        Player_TimerManager.Instance.AddTimer(
            _grappingHook.AttachDelay,
            () =>
            {
                _grappingHook.SetJoint();
                _shouldCheck = true;
            }
        );
    }

    public override void PhysicsUpdate()
    {
        _realDist = Vector2.Distance(_player.transform.position, _grappingHook.HookPoint.transform.position);
        // Check if the grapping line is broken
        CheckGLineBreak();

        _dashSkill.CheckLineDash();
        if (_shouldMove)
            _grappingHook.MoveOnGLine();
        else if (_realDist < _grappingHook.RopeJoint.distance + 0.5f)
            _grappingHook.RopeJoint.distance = _realDist;

        if (_shouldAddForce)
            _player.Rb.AddForce(new Vector2(
                _player.InputSys.MoveInput.x * _grappingHook.LineSwingForce,
                0f
            ), ForceMode2D.Force);
                
        if (_shouldCheck)
            GroundMove();
    }
    public override void LogicUpdate()
    {
        /* Release hook when button is released and currently attached or
        when grapping line is broken*/
        if ((!_player.InputSys.GrapperTrigger && _player.IsAttached) ||
            _checker.GLineChecker.IsTouchingLayers(_checker.GLineBreakLayer)
        )
        _grappingHook.ReleaseGHook();

        _shouldMove = !_player.InputSys.DashTrigger && _realDist >= _grappingHook.RopeJoint.distance - 0.5f;
        // If current velocity less than max speed, can add force
        _shouldAddForce = Mathf.Abs(_player.Rb.linearVelocity.magnitude) < _grappingHook.MaxSwingSpeed;

        // Update line renderer position
        _grappingHook.RopeLine.SetPosition(0, _player.transform.position);
    }

    public override void Exit()
    {
        base.Exit();
       _grappingHook.CoolDownSkill();
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
    void GroundMove()
    {
        if (!_player.Checker.IsGrounded)
            return;

        float ropeDistance = _grappingHook.RopeJoint.distance;
        _grappingHook.RopeJoint.distance -= _grappingHook.LineGroundMoveForce * Time.fixedDeltaTime;

            if (!_player.Checker.IsGrounded ||
                    _realDist >= ropeDistance + 0.2f)
                _grappingHook.SetJoint();
    }
}