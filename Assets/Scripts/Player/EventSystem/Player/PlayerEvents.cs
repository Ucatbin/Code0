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
// Attack
public struct P_Skill_AttackPressed : ISkillEvent
{
    public P_AttackModel Skill;
    public Vector3 InputDirection;
}
public struct P_Skill_AttackExecute : ISkillEvent
{
    public Vector3 AttackDirection;
}
// DoubleJump
public struct P_Skill_DoubleJumpPressed : ISkillEvent
{
    public P_DoubleJumpModel Skill;
}
public struct P_Skill_DoubleJumpExecute : ISkillEvent
{
    public float DoubleJumpSpeed;
}

// GrappingHook
public struct P_Skill_GrappingHookPressed : ISkillEvent
{
    public P_GrappingHookModel Skill;
    public Vector3 CurrentPosition;
    public Vector3 InputDirection;
    public bool IsGrounded;
}
public struct P_Skill_GrappingHookExecute : ISkillEvent
{
    public bool IsGrounded;
    public Vector3 TargetPosition;
}
public struct P_Skill_GrappingHookReleased : ISkillEvent
{
    public P_GrappingHookModel Skill;
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
public struct BeKilled
{
    
}
#endregion
#region Buffs
// Countdown
public struct UpdateCountdownDisplay
{
    public float TimerDisplay;
}
#endregion