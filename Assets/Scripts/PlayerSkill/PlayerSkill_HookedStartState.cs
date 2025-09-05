using UnityEngine;

public class PlayerSkill_HookedStartState : Player_BaseState
{
    PlayerSkill_GrappingHook _grappingHook;

    public PlayerSkill_HookedStartState(PlayerController entity, StateMachine stateMachine, string stateName) : base(entity, stateMachine, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        // Initialize grappling hook component
        _grappingHook = Player_SkillManager.Instance.GrappingHook;

        _grappingHook.ApplyAttachForce();

        Player_TimerManager.Instance.AddTimer(
            0.15f,
            () => { _stateMachine.ChangeState(_player.HookedState, true); },
            "Player_AbilityTimer"
        );
    }
    public override void PhysicsUpdate() { }
    public override void LogicUpdate() { }
    public override void Exit()
    {
        base.Exit();
    }
}
