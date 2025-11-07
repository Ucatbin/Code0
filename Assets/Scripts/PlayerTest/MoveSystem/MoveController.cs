using ThisGame.Core;
using ThisGame.Entity.MoveSystem;
using UnityEngine;

public class MoveController : BaseController
{
    [SerializeField] MoveData _data;
    public MoveModel Model;

    public override void Initialize()
    {
        Model = new MoveModel(_data);
    }
}

#region MoveEvents
public struct MoveButtonPressed
{
    public Vector3 MoveDirection;
}
public struct JumpButtonPressed { }
public struct JumpButtonReleased { }

#endregion