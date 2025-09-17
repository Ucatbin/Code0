using UnityEngine;
using UnityEngine.Pool;

public abstract class BaseObjectPool : MonoBehaviour
{
	public ObjectPool<GameObject> Pool;
	[SerializeField] protected GameObject _objPrefab;
    [SerializeField] protected int _minCount;
    [SerializeField] protected int _maxCount;
	
	protected virtual void Awake()
	{
		Pool = new ObjectPool<GameObject>(
	
			createFunc,
	
			actionOnGet,
	
			actionOnRelease,
	
			actionOnDestroy,
	
			false,
	
			_minCount,
	
			_maxCount
	
		);
	}
	
	protected virtual GameObject createFunc() => Instantiate(
		_objPrefab,
		transform.position,
		Quaternion.identity,
		transform
	);

    protected abstract void actionOnGet(GameObject obj);
    protected abstract void actionOnRelease(GameObject obj);
    protected abstract void actionOnDestroy(GameObject obj);
}