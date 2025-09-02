using UnityEngine;

public class HookPointPool : BaseObjectPool
{
    protected override GameObject createFunc() => Instantiate(
        _objPrefab,
        transform.position,
        Quaternion.identity,
        transform
    );

    protected override void actionOnGet(GameObject obj)
    {
        obj.SetActive(true);
    }

    protected override void actionOnRelease(GameObject obj)
    {
        obj.SetActive(false);
    }
    protected override void actionOnDestroy(GameObject obj)
    {
        Destroy(obj);
    }
}