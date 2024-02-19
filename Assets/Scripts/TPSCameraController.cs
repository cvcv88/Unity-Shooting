using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TPSCameraController : MonoBehaviour
{
	[SerializeField] Transform cameraRoot;
	[SerializeField] Transform target;
	[SerializeField] float distance;
	[SerializeField] float mouseSensitivity;

	private Vector2 inputDir;
	private float xRotation; // 위아래는 x

	private void OnEnable()
	{
		Cursor.lockState = CursorLockMode.Locked;
	}
	private void OnDisable()
	{
		Cursor.lockState = CursorLockMode.None;
	}

	private void Update()
	{
		SetTargetPos();
	}
	private void SetTargetPos()
	{
		target.position = Camera.main.transform.position + Camera.main.transform.forward * distance;
	}

	// 모든 진행상황이 진행된 후에 진행되기 때문에 플레이어의 어깨 위치 정해진 후에 행동한다.
	private void LateUpdate() 
	{
		xRotation -= inputDir.y * mouseSensitivity * Time.deltaTime;
		xRotation = Mathf.Clamp(xRotation, -80f, 80f);

		transform.Rotate(Vector3.up, inputDir.x * mouseSensitivity * Time.deltaTime);
		cameraRoot.localRotation = Quaternion.Euler(xRotation, 0, 0);
	}

	private void OnLook(InputValue inputValue)
	{
		inputDir = inputValue.Get<Vector2>();
	}
}
