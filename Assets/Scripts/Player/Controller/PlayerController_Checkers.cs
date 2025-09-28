using UnityEngine;

public class PlayerController_Checkers : CheckerController
{
    public override void WallCheck()
    {
        if (IsGrounded)
            return;
        base.WallCheck();
    }
}