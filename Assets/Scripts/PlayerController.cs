using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	[Header("Component")]
	[SerializeField] CharacterController controller;
	// drag and drop �� �� �ֵ��� srializeField ����
	// ������ �޶��� ��, �������� rigidbody����.
	// FixedUpdate���� rigid.AddForce(moveDir*moveSpeed)��ŭ �̵��߾���.

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
		// Update ���� �����ϴϱ� Time.deltaTime �߰����ֱ�

		controller.Move(transform.right * moveDir.x * moveSpeed * Time.deltaTime); // �����ִ� ���⿡���� right
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
		moveDir.x = inputDir.x; // �¿� �翷
		moveDir.z = inputDir.y; // �յ�(moveDir.y�� ���Ʒ�)
	}
	private void OnJump(InputValue value)
	{
		ySpeed = jumpSpeed;
	}
}
