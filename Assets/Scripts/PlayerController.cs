using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	[Header("Component")]
	[SerializeField] CharacterController controller;
	// drag and drop 할 수 있도록 srializeField 설정
	// 기존과 달라진 점, 기존에는 rigidbody였음.
	// FixedUpdate에서 rigid.AddForce(moveDir*moveSpeed)만큼 이동했었다.

	[Header("Spec")]
	[SerializeField] float moveSpeed;
	[SerializeField] float jumpSpeed;

	private Vector3 moveDir;
	private float ySpeed;

	private void Update()
	{
		Move();
		JumpMove();
	}
	private void Move()
	{
		// controller.Move(moveDir * moveSpeed * Time.deltaTime);
		// Update 에서 수행하니까 Time.deltaTime 추가해주기

		controller.Move(transform.right * moveDir.x * moveSpeed * Time.deltaTime); // 보고있는 방향에서의 right
		controller.Move(transform.forward * moveDir.z * moveSpeed * Time.deltaTime); 
	}
	private void JumpMove()
	{
		ySpeed += Physics.gravity.y * Time.deltaTime;

		if (controller.isGrounded)
		{
			ySpeed = 0;
		}
		controller.Move(Vector3.up * ySpeed * Time.deltaTime);
	}
	private void OnMove(InputValue Value)
	{
		Vector2 inputDir = Value.Get<Vector2>();
		moveDir.x = inputDir.x; // 좌우 양옆
		moveDir.z = inputDir.y; // 앞뒤(moveDir.y는 위아래)
	}
	private void OnJump(InputValue value)
	{
		ySpeed = jumpSpeed;
	}
}
