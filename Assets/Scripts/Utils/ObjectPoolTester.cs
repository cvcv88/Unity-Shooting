using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolTester : MonoBehaviour
{
    [SerializeField] ObjectPooler pooler;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Manager.Pool.GetPool("ÃÑ¾Ë");

			//PooledObject instance = pooler.GetPool();
			// // PooledObject instance = Manager.pooler.GetPool("Bullet");
			//instance.transform.position = Random.insideUnitSphere * 10f;
		}
	}
}
