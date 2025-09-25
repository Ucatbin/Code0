using UnityEngine;

public interface IMoveable
{
    Vector2 TargetSpeed { get; }
    // Base property
    float BaseGroundSpeed { get; }
    float BaseAirSpeed { get; }

    // Buff modifier
    float AccelMult { get; set; }
    float GroundSpeedMult { get; set; }
    float AirSpeedMult { get; set; }

    float GroundSpeedBonus { get; set; }
    float AirSpeedBonus { get; set; }

    // Final property
    float FinalGroundSpeed { get; }
    float FinalAirSpeed { get; }

    void SetTargetSpeed(Vector2 speed);
    void AddSpeed(float speed);
    void MultSpeed(float multiplier);
}