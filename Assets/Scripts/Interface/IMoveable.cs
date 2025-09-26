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
    Vector2 TargetSpeed { get; }
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
    void SetTargetSpeed(Vector2 speed);
    /// <summary>
    /// Invoke when change speed buncer
    /// </summary>
    void AddSpeed(float speed);
    /// <summary>
    /// Invoke when change speed multiplier
    /// </summary>
    void MultSpeed(float multiplier);
    #endregion
}