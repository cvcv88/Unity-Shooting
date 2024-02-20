using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
	[SerializeField] Gun[] guns;

	private Gun curGun; // 현재 총

	private void Start()
	{
		curGun = guns[0]; // 현재 총은 총 배열에서 제일 첫 번째 총
	}

	public void Fire()
	{
		curGun.Fire(); // 지금 잡고있는 총을 쏜다.
	}
}
