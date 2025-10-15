using System.Collections;
using UnityEngine;

public class Player_HookedState : Player_BaseState
{
    PlayerSkill_GrappingHook _gHookSkill;

    public Player_HookedState(PlayerController_Main entity, StateMachine stateMachine, int priority, string stateName) : base(entity, stateMachine, priority, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _gHookSkill = Player_SkillManager.Instance.GrappingHook;
    }

    public override void PhysicsUpdate()
    {
        if (!_gHookSkill.IsHookFinished)
            return;

        if (_player.Rb.linearVelocityY == 0)
        {
            _player.SetTargetVelocityY(0);
            _player.ApplyMovement();
        }
        
        _gHookSkill.MoveOnLine();
    }
    public override void LogicUpdate()
    {
        _gHookSkill.CheckLineBreak();
        _gHookSkill.SetLineRenderer();
        
        if (!_gHookSkill.IsHookFinished)
            return;

        _player.ApplyMovement();

        if (_player.CheckerSys.IsGrounded)
            _gHookSkill.ReleaseGHook();

    }

    public override void Exit()
    {
        base.Exit();

        _player.Rb.gravityScale = 0;

        _player.IsHooked = false;
        _player.IsBusy = false;
        _player.IsPhysicsDriven = false;
    }
}