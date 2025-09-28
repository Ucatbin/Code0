using UnityEngine;

public interface IGroundCheck
{
    bool IsGrounded { get; }
    Transform GroundCheckPoint { get; }
    LayerMask GroundLayer { get; }
    float GroundCheckDist { get; }
    float GroundCheckWidth { get; }
    void GroundCheck() { }
}