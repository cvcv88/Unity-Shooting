using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FPSCameraController : MonoBehaviour
{
    // 1. 우선 마우스 움직인만큼의 입력 받아야 한다.
    // 2. 

    [SerializeField] Transform cameraRoot;
    [SerializeField] float mouseSensitivity;

    private Vector2 inputDir; // 앞뒤아니고, 위아래왼쪽오른쪽이기 때문에 그대로 받아도됨
	private float xRotation;

	private void OnEnable()
	{
		Cursor.lockState = CursorLockMode.Locked; // 마우스가 계속 정가운데에 고정되게 설정
	}
	private void OnDisable() // 비활성화 되었을 때
	{
		Cursor.lockState = CursorLockMode.None;
		// Confined : 화면 창 밖으로 마우스 못 끌고 나가게 하는 모드
	}

	private void Update()
	{
		xRotation -= inputDir.y * mouseSensitivity * Time.deltaTime;
		// x의 양수값이 아래 방향을 보는거라서 - 붙여준다.
		xRotation = Mathf.Clamp(xRotation, 80f, 80f);

        // y 기준으로 돌려야 좌우
        transform.Rotate(Vector3.up, mouseSensitivity * inputDir.y * Time.deltaTime);

		// x 기준으로 돌려야 위아래
		// cameraRoot.Rotate(Vector3.right, mouseSensitivity * -inputDir.y * Time.deltaTime);

		// cameraRoot.localRotation = new Vector3(0, xRotation, 0); // 쿼터니언이라 y 값만 따로 변경 불가능함 ㅠ
		cameraRoot.localRotation = Quaternion.Euler(0, xRotation, 0); // 쿼터니언의 오일러를 변환해서 써주자.
	}

	private void OnLook(InputValue value)
    {
        // Debug.Log(value.Get<Vector2>());
        inputDir = value.Get<Vector2>();    
    }
}
