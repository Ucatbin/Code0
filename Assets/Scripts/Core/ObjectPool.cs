using UnityEngine;
using UnityEngine.Pool;

public abstract class ObjectPool : MonoBehaviour
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

	protected virtual void actionOnGet(GameObject obj)
	{
		obj.SetActive(true);
	}
	protected virtual void actionOnRelease(GameObject obj)
	{
		obj.SetActive(false);
	}
	protected virtual void actionOnDestroy(GameObject obj)
	{
		Destroy(obj);
	}
}