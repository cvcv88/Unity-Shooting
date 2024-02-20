using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
	[SerializeField] Transform muzzlePoint; // �Ѿ� ������ ��ġ
	[SerializeField] int damage;
	[SerializeField] float maxDistance; // �ִ� �Ÿ�
	[SerializeField] LayerMask layerMask;
	[SerializeField] ParticleSystem muzzleFlash;
	[SerializeField] ParticleSystem hitEffect;

	[SerializeField] Transform hitPoint; // �Ѿ��� ���� �κ�
	public void Fire()
	{
		muzzleFlash.Play();
		// Debug.DrawRay(muzzlePoint.position, muzzlePoint.forward * maxDistance, Color.red, 0.1f);
		// Debug.Log("����");
		// ��ȯ�� bool �ε������� �ƴ��� ���� ����
		// ��� �� ����, ��� �������� �� ����, out parameter, (�ִ�Ÿ�)
		if(Physics.Raycast(muzzlePoint.position, muzzlePoint.forward, out RaycastHit hitInfo, maxDistance))
		{
			// Debug.Log("���� ���� ��� ���̵� �¾Ҵ�.");
			Debug.Log(hitInfo.collider.gameObject.name); // �浹 ���� ������Ʈ�� �̸�
			Debug.Log(hitInfo.distance);
			hitPoint.position = hitInfo.point;
			Debug.DrawRay(muzzlePoint.position, muzzlePoint.forward * hitInfo.distance, Color.red, 0.1f);
			// Target target = hitInfo.collider.GetComponent<Target>(); // �ε���, �浹�� �浹ü�� Target Component ��������
			IDamagable damagable = hitInfo.collider.GetComponent<IDamagable>(); // �ε���, �浹�� �浹ü�� IDamagable Component ��������

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
			Debug.Log("���� ���µ� �ƹ� �͵� �ȸ¾Ҵ�.");
			Debug.DrawRay(muzzlePoint.position, muzzlePoint.forward * maxDistance, Color.red, 0.1f);
		}
	}
}
