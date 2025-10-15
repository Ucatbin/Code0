using UnityEngine;

public class Target : EnemyController_Main
{
    public override void HandleMovement()
    {
        base.HandleMovement();

        int moveDir = IsPatroling ? FacingDir : 0;
        float speedX = Mathf.MoveTowards(
            TargetVelocity.x,
            moveDir != 0 ? FinalGroundSpeed * moveDir : 0,
            PropertySO.Accel
        );
        SetTargetVelocity(new Vector2(speedX, TargetVelocity.y));
        Rb.linearVelocity = new Vector2(TargetVelocity.x, Rb.linearVelocityY);
    }
}