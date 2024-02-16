using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FPSCameraController : MonoBehaviour
{
    // 1. �켱 ���콺 �����θ�ŭ�� �Է� �޾ƾ� �Ѵ�.
    // 2. 

    [SerializeField] Transform cameraRoot;
    [SerializeField] float mouseSensitivity;

    private Vector2 inputDir; // �յھƴϰ�, ���Ʒ����ʿ������̱� ������ �״�� �޾Ƶ���
	private float xRotation;

	private void OnEnable()
	{
		Cursor.lockState = CursorLockMode.Locked; // ���콺�� ��� ������� �����ǰ� ����
	}
	private void OnDisable() // ��Ȱ��ȭ �Ǿ��� ��
	{
		Cursor.lockState = CursorLockMode.None;
		// Confined : ȭ�� â ������ ���콺 �� ���� ������ �ϴ� ���
	}

	private void Update()
	{
		xRotation -= inputDir.y * mouseSensitivity * Time.deltaTime;
		// x�� ������� �Ʒ� ������ ���°Ŷ� - �ٿ��ش�.
		xRotation = Mathf.Clamp(xRotation, 80f, 80f);

        // y �������� ������ �¿�
        transform.Rotate(Vector3.up, mouseSensitivity * inputDir.y * Time.deltaTime);

		// x �������� ������ ���Ʒ�
		// cameraRoot.Rotate(Vector3.right, mouseSensitivity * -inputDir.y * Time.deltaTime);

		// cameraRoot.localRotation = new Vector3(0, xRotation, 0); // ���ʹϾ��̶� y ���� ���� ���� �Ұ����� ��
		cameraRoot.localRotation = Quaternion.Euler(0, xRotation, 0); // ���ʹϾ��� ���Ϸ��� ��ȯ�ؼ� ������.
	}

	private void OnLook(InputValue value)
    {
        // Debug.Log(value.Get<Vector2>());
        inputDir = value.Get<Vector2>();    
    }
}
