using OjbectPoolTest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
	[SerializeField] PooledObject prefab;
	[SerializeField] int poolsize;

	private Stack<PooledObject> objectPool;

	private void Awake()
	{
		CreatePool();
	}
	public void CreatePool()
	{
		objectPool = new Stack<PooledObject>(poolsize);
		for (int i = 0; i < poolsize; i++)
		{
			PooledObject instance = Instantiate(prefab);
			instance.gameObject.SetActive(false);
			instance.pooler = this; // ?
			instance.transform.parent = transform; // 자식으로 설정하기
			objectPool.Push(instance);
		}
	}
	public PooledObject GetPool()
	{
		if (objectPool.Count > 0)
		{
			PooledObject instance = objectPool.Pop();
			instance.gameObject.SetActive(true);
			instance.transform.parent = null;
			return instance;
		}
		else
		{
			PooledObject instance = Instantiate(prefab);
			instance.pooler = this; // ?
			return Instantiate(prefab);
		}
	}

	public void ReturnPool(PooledObject instance)
	{
		if (objectPool.Count < poolsize)
		{
			instance.gameObject.SetActive(false);
			instance.transform.parent = transform;
			objectPool.Push(instance);
		}
		else
		{
			Destroy(instance.gameObject);
		}
	}
}
