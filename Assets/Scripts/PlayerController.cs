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
		// Update 에서 수행하니까 Time.deltaTime 추가해주기

		/*controller.Move(transform.right * moveDir.x * moveSpeed * Time.deltaTime); // 보고있는 방향에서의 right
		controller.Move(transform.forward * moveDir.z * moveSpeed * Time.deltaTime);

		animator.SetFloat("MoveSpeed", moveDir.magnitude * moveSpeed); // 누른만큼의 크기(방향) * moveSpeed*/

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
		moveDir.x = inputDir.x; // 좌우 양옆
		moveDir.z = inputDir.y; // 앞뒤(moveDir.y는 위아래)
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
		// 총쏘는 로직 구현
		animator.SetTrigger("Fire");
	}

	private void OnReload(InputValue value)
	{
		Reload();
	}	
	public void Reload()
	{
		// 재장전 로직 구현
		animator.SetTrigger("Reload");
	}


}
