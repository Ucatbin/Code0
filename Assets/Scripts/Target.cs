using UnityEngine;

public class Target : EnemyController_Main
{
    public override void HandleMovement()
    {
        base.HandleMovement();

        int moveDir = IsPatroling ? FacingDir : 0;
        float speedX = Mathf.MoveTowards(
            TargetSpeed.x,
            moveDir != 0 ? FinalGroundSpeed * moveDir : 0,
            PropertySO.Accel
        );
        SetTargetSpeed(new Vector2(speedX, TargetSpeed.y));
        Rb.linearVelocity = new Vector2(TargetSpeed.x, Rb.linearVelocityY);
    }
}