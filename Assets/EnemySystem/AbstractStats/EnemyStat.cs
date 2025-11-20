using UnityEngine;

public abstract class EnemyStat: MonoBehaviour
{
    protected Rigidbody2D rb;
    public abstract void CheckStat();
    public abstract void Tick();
    public abstract void OnExit();

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
}
