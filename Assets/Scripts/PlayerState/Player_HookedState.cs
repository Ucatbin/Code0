using System.Collections;
using UnityEngine;

public class Player_HookedState : Player_BaseState
{
    GrappingHook _grappingHook;
    DistanceJoint2D _joint;
    PlayerChecker _checker;
    float _accelerate = 1f;

    public Player_HookedState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName) { }

    public override void Enter()
    {
        // Initialize grappling hook component
        _grappingHook = _player.GrappingHook;
        _joint = _grappingHook.DistanceJoint;
        _checker = _player.Checker;

        // Enable player collision check
        _checker.GLineChecker.enabled = true;

        _accelerate = 1f;
    }

    public override void PhysicsUpdate()
    {
        CheckGLineBreak();
        // Control the length of the grapping line
        ControlGLine();
        ControlAcceleration();
    }
    public override void LogicUpdate()
    {
        /* Release hook when button is released and currently attached or
        when grapping line is broken*/
        if ((!_player.InputSystem.GrapperTrigger && _player.IsAttached) ||
            _checker.GLineChecker.IsTouchingLayers(_checker.GLineBreakLayer))
        {
            ReleaseHook();
        }


        // Update line renderer position
        _grappingHook.LineRenderer.SetPosition(1, _player.transform.position);
    }

    public override void Exit()
    {
        // Disable player collision check
        _checker.GLineChecker.enabled = false;
    }

    void ReleaseHook()
    {
        // Let state machine know the player is released
        GrappleEvent.TriggerHookReleased();
        _player.IsAttached = false;

        // Disable distance joint and line renderer
        _joint.enabled = false;
        _grappingHook.LineRenderer.enabled = false;

    }
    void CheckGLineBreak()
    {
        if (_joint.distance > _grappingHook.MaxDetectDist)
        {
            ReleaseHook();
            return;
        }

        RaycastHit2D[] hits = new RaycastHit2D[2];
        int hitCount = Physics2D.RaycastNonAlloc(
            _player.transform.position,
            (_grappingHook.HookPoint - (Vector2)_player.transform.position).normalized,
            hits,
            _joint.distance,
            _grappingHook.CanHookLayer
        );

        if (hitCount > 1 || _joint.distance > _grappingHook.MaxDetectDist)
            ReleaseHook();
    }

    void ControlGLine()
    {
        float inputY = _player.InputSystem.MoveInput.y;
        bool sprint = _player.InputSystem.SprintTrigger;
        if (_grappingHook.CanUseGLineDash && sprint)
        {
            _accelerate = _grappingHook.GLineAcceleration;
            _grappingHook.StartCoroutine(_grappingHook.DashCDTimer(_grappingHook.GLineDashCD));
        }
        if (inputY != 0)
            _joint.distance -= _grappingHook.GLineSpeed * inputY * Time.fixedDeltaTime * _accelerate;
    }
    void ControlAcceleration()
    {
        if (_accelerate != 1f)
            _accelerate = Mathf.Lerp(_accelerate, 1f, Time.fixedDeltaTime * _grappingHook.GLineDamping);
    }
}