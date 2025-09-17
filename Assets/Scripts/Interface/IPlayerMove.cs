public interface IPlayerMove
{
    // 移动控制
    void Move(float inputX);
    void Stop();
    
    // 移动属性
    float CurrentSpeed { get; }
    float MaxSpeed { get; set; }
    float Acceleration { get; set; }
    float Damping { get; set; }
    
    // 状态查询
    bool IsMoving { get; }
    bool IsGrounded { get; }
}