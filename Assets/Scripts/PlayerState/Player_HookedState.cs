using System.Collections;
using UnityEngine;

public class Player_HookedState : Player_BaseState
{
    GrappingHook _grappingHook;
    public Player_HookedState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName) { }

    public override void Enter()
    {
        // Enable collision check
        _player.Checker.GrappingLineCheck.enabled = true;

        _grappingHook = _player.GrappingHook;
    }

    public override void PhysicsUpdate()
    {
        CheckGrappingLine();
    }
    public override void LogicUpdate()
    {
        // Release hook when button is released and currently attached
        if (!_player.InputSystem.GrapperTrigger && _player.IsAttached)
        {
            ReleaseHook();
        }
        // Release hook when grapping line is broken
        if (_player.Checker.GrappingLineCheck.IsTouchingLayers(_grappingHook.CanHookLayer))
        {
            ReleaseHook();
        }
        
        // Control the length of the grapping line
        ControlGrappingHook();

        // Update line renderer position
        _grappingHook.LineRenderer.SetPosition(1, _player.transform.position);

    }

    public override void Exit()
    {
        // Disable collision check
        _player.Checker.GrappingLineCheck.enabled = false;
    }

    void ReleaseHook()
    {
        // Let state machine know the player is released
        GrappleEvent.TriggerHookReleased();
        _player.IsAttached = false;

        // Disable distance joint and line renderer
        _grappingHook._distanceJoint.enabled = false;
        _grappingHook.LineRenderer.enabled = false;

    }
    void CheckGrappingLine()
    {
        RaycastHit2D[] hits = new RaycastHit2D[2];
        int hitCount = Physics2D.RaycastNonAlloc(
            _player.transform.position,
            (_grappingHook.HookPoint - (Vector2)_player.transform.position).normalized,
            hits,
            _grappingHook._distanceJoint.distance,
            _grappingHook.CanHookLayer
        );

        if (hitCount > 1 || _grappingHook._distanceJoint.distance > _grappingHook.MaxDetectDist)
            ReleaseHook();
    }

    void ControlGrappingHook()
    {
        Vector2 input = _player.InputSystem.ScrollInput;
        _grappingHook._distanceJoint.distance = Mathf.Clamp(_grappingHook._distanceJoint.distance, 0.5f, _grappingHook.MaxDetectDist);
        _grappingHook._distanceJoint.distance += input.y * _grappingHook.ScrollSpeed * Time.fixedDeltaTime;
    }
}
