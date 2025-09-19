using UnityEngine;

public class RTPropertyController : MonoBehaviour
{
    public Vector2 TargetSpeed;
    // Base property
    float _baseGroundSpeed;
    float _baseAirSpeed;

    // Buff modifier
    public float AccelMult = 1f;
    public float GroundSpeedMult = 1f;
    public float AirSpeedMult = 1f;

    public float GroundSpeedBonus = 0f;
    public float AirSpeedBonus = 0f;

    // Final property
    public float FinalGroundSpeed => (_baseGroundSpeed + GroundSpeedBonus) * GroundSpeedMult;
    public float FinalAirSpeed => (_baseAirSpeed + AirSpeedBonus) * AirSpeedMult;

    public void Init(PlayerPropertySO property)
    {
        _baseGroundSpeed = property.MaxGroundMoveSpeed;
        _baseAirSpeed = property.MaxAirMoveSpeed;
    }
}
