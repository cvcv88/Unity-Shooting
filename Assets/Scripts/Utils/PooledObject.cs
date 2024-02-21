using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
	public ObjectPooler pooler;
	[SerializeField] bool autoRelease;
	[SerializeField] float releaseTime;


	public void OnEnable() // ?
	{
		if (autoRelease)
		{
			releaseRoutine = StartCoroutine(RelaseRoutine());
		}
	}
	public void OnDisable()
	{
		if (autoRelease)
		{
			StopCoroutine(releaseRoutine);
		}
	}
	/*private void Update()
	{
		if(Input.GetKeyDown(KeyCode.A))
		{
			Release();
		}
	}*/

	Coroutine releaseRoutine;
	IEnumerator RelaseRoutine()
	{
		yield return new WaitForSeconds(releaseTime);
		Release();
	}

	public void Release()
	{
        if (pooler != null)
        {
			pooler.ReturnPool(this); // ³ª ¹Ý³³ÇÏ±â
		}
		else
		{
			Destroy(gameObject);
		}
       
	}
}
