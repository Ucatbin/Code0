using ThisGame.AbilitySystem;
using UnityEngine;

[CreateAssetMenu(fileName = "JumpAbilityData", menuName = "Game/AbilitySystem/Player/New JumpAbilityData")]
public class JumpAbilityData : AbilityData
{
    [Header("Jump")]
    public float JumpInitPower = 14f;
    public float JumpInputWindow = 0.25f;
    public float JumpHoldPower = 10f;
    public float CoyoteWindow = 0.15f;
    [Header("WallJump")]
    public float WallJumpPower = 14f;
    public float WallJumpWindow = 0.15f;
    public Vector2 WallJumpDir = new Vector2(0.8f, 1f);
}
