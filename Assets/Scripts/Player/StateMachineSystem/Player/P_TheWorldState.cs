using System;
using ThisGame.Core.CheckerSystem;
using ThisGame.Entity.EntitySystem;
using ThisGame.Entity.MoveSystem;
using ThisGame.Entity.SkillSystem;
using ThisGame.Entity.StateMachineSystem;
using UnityEngine;

public class P_TheWorldState : P_BaseState
{
    P_TheWorldModel _skill;
    P_TheWorldData _data;
    public P_TheWorldState(PlayerController entity, StateMachine stateMachine, string animName, CheckerController checkers, MoveModel movement, P_TheWorldModel skill) : base(entity, stateMachine, animName, checkers, movement)
    {
        _skill = skill;
        _data = _skill.Data as P_TheWorldData;
    }

    protected override Type[] GetEvents()
    {
        throw new NotImplementedException();
    }
}
