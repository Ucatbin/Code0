using UnityEngine;

public class PlayerController_Checkers : CheckerHandler
{
    public override void WallCheck()
    {
        if (IsGrounded)
            return;
        base.WallCheck();
    }
}