using System;
using ThisGame.Entity.SkillSystem;
using ThisGame.Entity.StateMachineSystem;
using UnityEngine;

#region Abilities
// Move
public struct MoveButtonPressed : IPlayerInputEvent
{
    public Vector3 MoveDirection;
}
public struct MoveButtonRelease : IPlayerInputEvent { }
// Jump
public enum JumpType
{
    Jump,
    WallJump,
    DoubleJump
}
public struct JumpButtonPressed : IPlayerInputEvent { }
public struct JumpExecute : IPlayerInputEvent
{
    public JumpType JumpType;
    public Vector3 JumpDir;
}
public struct JumpButtonRelease : IPlayerInputEvent { }
public struct FlipAction
{
    public int FacingDir;
}
#endregion
#region Skills
public struct P_SkillPressed : ISkillPressedEvent
{
    public SkillModel Skill;
    public Vector3 InputDirection;

    public float PressTime;
    public Vector3 PlayerPosition;
}
public struct P_SkillExecute : ISkillEvent
{
    public SkillModel Skill;
    public Vector3 InputDirection;

    public float StartTime;
    public Vector3 PlayerPosition;
}
public struct P_SkillReleased : ISkillPressedEvent
{
    public SkillModel Skill;
}
public struct P_SkillStateSwitch
{
    public Type SkillState;
}

public struct P_Skill_RopeDashPressed : ISkillEvent
{
    public P_GrappingHookModel Skill;
}
public struct P_Skill_RopeDashExecute : ISkillEvent
{
    public int FacingDir;
}

#endregion
#region Checkers
public struct GroundCheckChange
{
    public bool ChangeToGrounded;
}
public struct WallCheckChange
{
    public bool ChangeToWalled;
}
#endregion

#region Universal
public struct ViewFlip
{
    public int FacingDir;
}
public struct StateChange
{
    public string LastStateAnim;
    public string NewStateAnim;
}
public struct BeHit
{
    public GameObject TargetEntity;
}
public struct BeKilled
{
    public GameObject TargetEntity;
    public GameObject Killer;
}
#endregion
#region Buffs
// Countdown
public struct UpdateCountdownDisplay
{
    public float TimerDisplay;
}
#endregion