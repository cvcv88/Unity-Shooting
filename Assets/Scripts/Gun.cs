using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
	[SerializeField] Transform muzzlePoint; // 총알 나가는 위치
	[SerializeField] int damage;
	[SerializeField] float maxDistance; // 최대 거리
	[SerializeField] LayerMask layerMask;
	[SerializeField] ParticleSystem muzzleFlash;
	[SerializeField] ParticleSystem hitEffect;

	[SerializeField] Transform hitPoint; // 총알이 맞은 부분
	public void Fire()
	{
		muzzleFlash.Play();
		// Debug.DrawRay(muzzlePoint.position, muzzlePoint.forward * maxDistance, Color.red, 0.1f);
		// Debug.Log("탕탕");
		// 반환형 bool 부딪혔는지 아닌지 여부 나옴
		// 어디서 쏠 건지, 어디 방향으로 쏠 건지, out parameter, (최대거리)
		if(Physics.Raycast(muzzlePoint.position, muzzlePoint.forward, out RaycastHit hitInfo, maxDistance))
		{
			// Debug.Log("총을 쏴서 어느 것이든 맞았다.");
			Debug.Log(hitInfo.collider.gameObject.name); // 충돌 게임 오브젝트의 이름
			Debug.Log(hitInfo.distance);
			hitPoint.position = hitInfo.point;
			Debug.DrawRay(muzzlePoint.position, muzzlePoint.forward * hitInfo.distance, Color.red, 0.1f);
			// Target target = hitInfo.collider.GetComponent<Target>(); // 부딪힌, 충돌한 충돌체의 Target Component 가져오기
			IDamagable damagable = hitInfo.collider.GetComponent<IDamagable>(); // 부딪힌, 충돌한 충돌체의 IDamagable Component 가져오기

			//if(target != null)
			//{
			damagable?.TakeDamage(damage);
			//}

			// Instantiate(hitEffect, hitInfo.point, Quaternion.identity);
			ParticleSystem effect = Instantiate(hitEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
			effect.transform.parent = hitInfo.transform;

			Rigidbody rigid = hitInfo.collider.GetComponent<Rigidbody>();
			if(rigid != null)
			{
				rigid.AddForceAtPosition(-hitInfo.normal * 10f, hitInfo.point, ForceMode.Impulse);
			}
		}
		else
		{
			Debug.Log("총을 쐈는데 아무 것도 안맞았다.");
			Debug.DrawRay(muzzlePoint.position, muzzlePoint.forward * maxDistance, Color.red, 0.1f);
		}
	}
}
