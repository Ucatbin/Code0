using UnityEngine;

[CreateAssetMenu(fileName = "GlobalVarSO", menuName = "Game/GlobalVar")]
public class GlobalVarSO : ScriptableObject
{
    [Header("MOVEMENT")]
    public float MaxGroundSpeed = 12f;
    public float MaxAirSpeed = 20f;
    public float MaxFallSpeed = 20f;
    [Header("GRAVITY")]
    public float ZeroGravity = 0f;
    public float DefaultGravity = 1f;
    public float FallDamping = 60f;
    public float FallMult = 1.5f;
}