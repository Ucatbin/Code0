using ThisGame.AbilitySystem;
using ThisGame.Events.AbilityEvents;
using Unity.VisualScripting;

public class Player_JumpState : Player_AirState
{
    JumpAbilityModel _jumpSkill;
    public Player_JumpState(PlayerController_Main entity, StateMachine stateMachine, int priority, string stateName) : base(entity, stateMachine, priority, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _jumpSkill = ServiceLocator.Get<AbilitySystemBootstrap>().AbilityPresenter.GetAbilityModel<JumpAbilityModel>("Jump");
        var eventBus = ServiceLocator.Get<IEventBus>();
        _player.SetTargetVelocityY(_jumpSkill.Data.JumpInitPower);
        _player.ApplyMovement();

        TimerManager.Instance.AddTimer(
            _jumpSkill.Data.JumpInputWindow,
            () => eventBus.Publish(new JumpExecuteTriggerEnd()),
            "JumpStateTimer"
        );

        if (_jumpSkill.CurrentCharges != _jumpSkill.Data.MaxCharges)
            eventBus.Publish(new JumpExecuteTriggerEnd());
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (_player.CheckerSys.IsWallDected)
        {
            var eventBus = ServiceLocator.Get<IEventBus>();
            eventBus.Publish(new JumpExecuteTriggerEnd());
        }
        else
        {
            _player.SetTargetVelocityY(_jumpSkill.Data.JumpHoldPower);
            _player.ApplyMovement();
        }        
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

    }

    public override void Exit()
    {
        base.Exit();

        TimerManager.Instance.CancelTimersWithTag("JumpStateTimer");

        _player.IsBusy = false;
        _player.IsJumping = false;
    }
}