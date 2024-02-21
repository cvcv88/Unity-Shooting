/*
 * 디자인 패턴 Object Pool
 */

/*
 * <오브젝트 풀>
 * 프로그램 내에서 빈번하게 재활용되는 많은 수의 인스턴스들을 생성 & 삭제를 하지 않고
 * 미리 생성해놓은 인스턴스들을 객체집합(풀)에 보관하고
 * 인스턴스를 대여 & 반납하는 방식으로 사용하는 기법
 * 
 * 구현
 * 1. 인스턴스들을 보관할 오브젝트 풀을 생성
 * 2. 프로그램의 시작시 오브젝트 풀에 인스턴스들을 생성하여 보관
 * 3. 인스턴스가 필요로 하는 상황에서 생성 대신 오브젝트 풀에서 인스턴스를 대여하여 사용
 * 4. 인스턴스가 필요로 하지 않는 상황에서 삭제 대신 오브젝트 풀에 인스턴스를 반납하여 보관
 * 
 * 장점
 * 1. 빈번하게 사용하는 인스턴스의 생성에 소요되는 오버헤드를 줄일 수 있음
 * 2. 빈번하게 사용하는 인스턴스의 삭제에 가비지 컬렉터의 부담을 줄일 수 있음
 * 
 * 주의점
 * 1. 미리 생성해놓은 인스턴스에 의해 사용하지 않는 경우에도 메모리를 차지하고 있음
 * 2. 메모리가 넉넉하지 않은 상황(기기)에서 너무 낳은 오브젝트 풀링을 적용하는 경우,
 *		힙 영역의 여유 공간이 줄어들어 오히려 가비지 컬렉터에 부담을 주어 프로그램이 느려지는 경우에 주의해야 함
 */

using System.Collections.Generic;
using UnityEngine;
namespace OjbectPoolTest
{
	public class ObjectPooler : MonoBehaviour
	{
		private PooledObject prefab;
		private Stack<PooledObject> objectPool;
		// List 사용 안하는 이유 : 맨 마지막거 꺼내오는 작업, 다시 돌려놓는 작업이 추가로 필요하기 때문에
		// 어차피 List 기반인 Stack 사용한다.
		private int poolSize = 100;

		public void CreatePool()
		{
			objectPool = new Stack<PooledObject>(poolSize);
			for (int i = 0; i < poolSize; i++)
			{
				PooledObject instance = Instantiate(prefab);
				instance.gameObject.SetActive(false); // 미리 생성하고나서 보관(push)하기 전에 비활성화 시켜놓기
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
		// 생성 & 삭제가 빈번한 클래스
	}
}