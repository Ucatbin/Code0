using UnityEngine;

public abstract class Locomotion2D : MonoBehaviour
{
    public abstract void MoveTowards(Vector2 targetPos, float speed);
    public abstract void Stop();
    public abstract void FaceTowards(Vector2 dir);
}

