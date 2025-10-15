using UnityEngine;

public interface IWallCheck
{
    bool IsWallDected { get; }
    Transform WallCheckPoint { get; }
    LayerMask WallCheckLayer { get; }
    float WallCheckDist { get; }
    float WallCheckWidth { get; }
    void WallCheck() {}
}
