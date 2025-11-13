using ThisGame.Entity.SkillSystem;
using UnityEngine;

#region Abilities
public struct MoveButtonPressed : IPlayerInputEvent
{
    public Vector3 MoveDirection;
}
public struct MoveButtonRelease : IPlayerInputEvent { }

public struct JumpButtonPressed : IPlayerInputEvent { }
public struct JumpExecute : IPlayerInputEvent
{
    public Vector3 JumpDir;
    public bool EndEarly;
}
public struct JumpButtonRelease : IPlayerInputEvent { }
public struct FlipAction
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

#region State
public struct StateChange
{
    public string LastStateAnim;
    public string NewStateAnim;
}
#endregion

#region Skills
// Attack
public struct P_Skill_AttackPressed : ISkillEvent
{
    public P_AttackModel Skill;
}
public struct P_SKill_AttackPrepare : ISkillEvent { }
public struct P_Skill_AttackExecute : ISkillEvent
{
    public P_AttackModel Skill;
}
// DoubleJump
public struct P_Skill_DoubleJumpPressed : ISkillEvent
{
    public P_DoubleJumpModel Skill;
}
public struct P_Skill_DoubleJumpPrepare : ISkillEvent
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
public struct P_Skill_GrappingHookPrepare : ISkillEvent
{
    public Vector3 TargetPosition;
}
public struct P_Skill_GrappingHookExecute : ISkillEvent
{
    public P_GrappingHookModel Skill;
    public bool IsGrounded;
    public Vector3 TargetPosition;
}
public struct P_Skill_GrappingHookReleased : ISkillEvent
{
    public P_GrappingHookModel Skill;
}
#endregion