using UnityEngine;

/// <summary>
/// Can Do Movement
/// </summary>
public interface IMoveable
{
    #region Basic
    /// <summary>
    /// The only variable need to handle movement, only try to get FinalSpeed
    /// </summary>
    Vector2 TargetVelocity { get; set; }
    /// <summary>
    /// Only get form property scriptable object
    /// </summary>
    float BaseGroundSpeed { get; }
    /// <summary>
    /// Only get form property scriptable object
    /// </summary>
    float BaseAirSpeed { get; }
    #endregion
    #region Buffer
    /// <summary>
    /// Accelerate Multiplier
    /// </summary>
    float AccelMult { get; set; }
    /// <summary>
    /// Speed multiplier
    /// </summary>
    float SpeedMult { get; set; }
    /// <summary>
    /// Speed bouncer
    /// </summary>
    float SpeedBonus { get; set; }
    /// <summary>
    /// Final ground speed calculated
    /// </summary>
    float FinalGroundSpeed { get; }
    /// <summary>
    /// Final air speed calculated
    /// </summary>
    float FinalAirSpeed { get; }
    #endregion

    #region Function
    /// <summary>
    /// Change speed quickly by adjust TargetSpeed directly
    /// </summary>
    void SetTargetVelocity(Vector2 speed);
    void SetTargetVelocityX(float speedX);
    void SetTargetVelocityY(float speedY);
    /// <summary>
    /// Invoke when change speed buncer
    /// </summary>
    void AddVelocity(float speed);
    /// <summary>
    /// Invoke when change speed multiplier
    /// </summary>
    void MultVelocity(float multiplier);
    void HandleMovement();
    void ApplyMovement();
    void HandleGravity();
    #endregion
}