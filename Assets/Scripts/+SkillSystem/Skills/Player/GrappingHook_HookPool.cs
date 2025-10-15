using UnityEngine;

public class HookPointPool : ObjectPool
{
    protected override void Awake()
    {
        InitHook();
        base.Awake();
    }

    protected override void actionOnRelease(GameObject obj)
    {
        base.actionOnRelease(obj);

        obj.transform.SetParent(transform);
    }
    void InitHook()
    {
        _objPrefab = new GameObject("Hook");
        _objPrefab.transform.SetParent(transform);
        _objPrefab.AddComponent<Rigidbody2D>();
        _objPrefab.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
    }
}