using UnityEngine;

public interface ICeilingCheck
{
    bool IsCeilingDetected { get; }
    Transform CeilingCheckPoint { get; }
    LayerMask CeilingLayer { get; }
    float CeilingCheckDist { get; }
    void CeilingCheck() {}
}
