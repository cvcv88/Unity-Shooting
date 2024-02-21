/*
 * ������ ���� Object Pool
 */

/*
 * <������Ʈ Ǯ>
 * ���α׷� ������ ����ϰ� ��Ȱ��Ǵ� ���� ���� �ν��Ͻ����� ���� & ������ ���� �ʰ�
 * �̸� �����س��� �ν��Ͻ����� ��ü����(Ǯ)�� �����ϰ�
 * �ν��Ͻ��� �뿩 & �ݳ��ϴ� ������� ����ϴ� ���
 * 
 * ����
 * 1. �ν��Ͻ����� ������ ������Ʈ Ǯ�� ����
 * 2. ���α׷��� ���۽� ������Ʈ Ǯ�� �ν��Ͻ����� �����Ͽ� ����
 * 3. �ν��Ͻ��� �ʿ�� �ϴ� ��Ȳ���� ���� ��� ������Ʈ Ǯ���� �ν��Ͻ��� �뿩�Ͽ� ���
 * 4. �ν��Ͻ��� �ʿ�� ���� �ʴ� ��Ȳ���� ���� ��� ������Ʈ Ǯ�� �ν��Ͻ��� �ݳ��Ͽ� ����
 * 
 * ����
 * 1. ����ϰ� ����ϴ� �ν��Ͻ��� ������ �ҿ�Ǵ� ������带 ���� �� ����
 * 2. ����ϰ� ����ϴ� �ν��Ͻ��� ������ ������ �÷����� �δ��� ���� �� ����
 * 
 * ������
 * 1. �̸� �����س��� �ν��Ͻ��� ���� ������� �ʴ� ��쿡�� �޸𸮸� �����ϰ� ����
 * 2. �޸𸮰� �˳����� ���� ��Ȳ(���)���� �ʹ� ���� ������Ʈ Ǯ���� �����ϴ� ���,
 *		�� ������ ���� ������ �پ��� ������ ������ �÷��Ϳ� �δ��� �־� ���α׷��� �������� ��쿡 �����ؾ� ��
 */

using System.Collections.Generic;
using UnityEngine;
namespace OjbectPoolTest
{
	public class ObjectPooler : MonoBehaviour
	{
		private PooledObject prefab;
		private Stack<PooledObject> objectPool;
		// List ��� ���ϴ� ���� : �� �������� �������� �۾�, �ٽ� �������� �۾��� �߰��� �ʿ��ϱ� ������
		// ������ List ����� Stack ����Ѵ�.
		private int poolSize = 100;

		public void CreatePool()
		{
			objectPool = new Stack<PooledObject>(poolSize);
			for (int i = 0; i < poolSize; i++)
			{
				PooledObject instance = Instantiate(prefab);
				instance.gameObject.SetActive(false); // �̸� �����ϰ��� ����(push)�ϱ� ���� ��Ȱ��ȭ ���ѳ���
				objectPool.Push(instance);
			}
		}

		public PooledObject GetPool()
		{
			if (objectPool.Count > 0)
			{
				PooledObject instance = objectPool.Pop();
				instance.gameObject.SetActive(true);
				return instance;
			}
			else
			{
				return Instantiate(prefab);
			}
		}

		public void ReturnPool(PooledObject instance)
		{
			if (objectPool.Count < poolSize)
			{
				instance.gameObject.SetActive(false);
				objectPool.Push(instance);
			}
			else
			{
				Destroy(instance.gameObject);
			}
		}
	}

	public class PooledObject : MonoBehaviour
	{
		// ���� & ������ ����� Ŭ����
	}
}