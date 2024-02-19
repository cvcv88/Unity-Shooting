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
	[SerializeField] Animator animator;

	[Header("Spec")]
	[SerializeField] float moveSpeed;
	[SerializeField] float walkSpeed;
	[SerializeField] float jumpSpeed;

	private Vector3 moveDir;
	private float ySpeed;
	private bool isWalk;

	private void Update()
	{
		Move();
		JumpMove();
	}
	private void Move()
	{
		// controller.Move(moveDir * moveSpeed * Time.deltaTime);
		// Update ���� �����ϴϱ� Time.deltaTime �߰����ֱ�

		/*controller.Move(transform.right * moveDir.x * moveSpeed * Time.deltaTime); // �����ִ� ���⿡���� right
		controller.Move(transform.forward * moveDir.z * moveSpeed * Time.deltaTime);

		animator.SetFloat("MoveSpeed", moveDir.magnitude * moveSpeed); // ������ŭ�� ũ��(����) * moveSpeed*/

		if (isWalk)
		{
			controller.Move(transform.right * moveDir.x * walkSpeed * Time.deltaTime);
			controller.Move(transform.forward * moveDir.z * walkSpeed * Time.deltaTime);
			animator.SetFloat("XSpeed", moveDir.x * walkSpeed, 0.1f, Time.deltaTime);
			animator.SetFloat("YSpeed", moveDir.z * walkSpeed, 0.1f, Time.deltaTime);
			//animator.SetFloat("MoveSpeed", moveDir.magnitude * walkSpeed, 0.1f, Time.deltaTime);
		}
		else
		{
			controller.Move(transform.right * moveDir.x * moveSpeed * Time.deltaTime);
			controller.Move(transform.forward * moveDir.z * moveSpeed * Time.deltaTime);
			animator.SetFloat("XSpeed", moveDir.x * moveSpeed, 0.1f, Time.deltaTime);
			animator.SetFloat("YSpeed", moveDir.z * moveSpeed, 0.1f, Time.deltaTime);
			//animator.SetFloat("MoveSpeed", moveDir.magnitude * moveSpeed, 0.1f, Time.deltaTime);
		}

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

	private void OnWalk(InputValue value)
	{
		if (value.isPressed)
		{
			isWalk = true;
		}
		else
		{
			isWalk = false;
		}
	}

	private void OnFire(InputValue value)
	{
		Fire();
	}

	public void Fire()
	{
		// �ѽ�� ���� ����
		animator.SetTrigger("Fire");
	}

	private void OnReload(InputValue value)
	{
		Reload();
	}	
	public void Reload()
	{
		// ������ ���� ����
		animator.SetTrigger("Reload");
	}


}
