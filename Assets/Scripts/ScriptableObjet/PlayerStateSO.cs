using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PlayerStateSO", menuName = "Game/Player/StateMachine")]
public class PlayerStateSO : ScriptableObject
{
    [Header("StateMachine")]
    public Player_IdleState IdleState { get; private set; }
    public Player_MoveState MoveState { get; private set; }
    public Player_AirState AirState { get; private set; }
    public Player_JumpState JumpState { get; private set; }
    public Player_CoyoteState CoyoteState { get; private set; }
    public Player_FallState FallState { get; private set; }
    public Player_AirGlideState AirGlideState { get; private set; }

    public Player_HookedState HookedState { get; private set; }
    public Player_AttackState AttackState { get; private set; }

    [Header("Movement States Priority")]
    [Min(0)] public int IdlePriority = 1;
    [Min(0)] public int MovePriority = 1;
    [Min(0)] public int AirPriority = 1;
    [Min(0)] public int JumpPriority = 2;
    [Min(0)] public int CoyotePriority = 1;
    [Min(0)] public int FallPriority = 1;
    [Min(0)] public int AirGlidePriority = 2;

    [Header("Action States Priority")]
    [Min(0)] public int HookedPriority = 3;
    [Min(0)] public int AttackPriority = 3;
    [Min(0)] public int DashPriority = 3;
    [Min(0)] public int UltimatePriority = 5;

    public void InstanceState(PlayerController_Main player, StateMachine stateMachine)
    {
        IdleState = new Player_IdleState(player, stateMachine, IdlePriority, "Idle");
        MoveState = new Player_MoveState(player, stateMachine, MovePriority, "Move");
        AirState = new Player_AirState(player, stateMachine, AirPriority, "Idle");
        JumpState = new Player_JumpState(player, stateMachine, JumpPriority, "Jump");
        CoyoteState = new Player_CoyoteState(player, stateMachine, CoyotePriority, "Idle");
        FallState = new Player_FallState(player, stateMachine, FallPriority, "Fall");
        AirGlideState = new Player_AirGlideState(player, stateMachine, AirGlidePriority, "Fall");

        HookedState = new Player_HookedState(player, stateMachine, HookedPriority, "Hooked");
        AttackState = new Player_AttackState(player, stateMachine, AttackPriority, "Attack");
    }
}
