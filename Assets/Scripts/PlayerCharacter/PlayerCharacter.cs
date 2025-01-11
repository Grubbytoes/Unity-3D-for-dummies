
using System;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerCharacter : MonoBehaviour
{
	public static event Action CollectedEnoughItems;
	public static event Action UiEscape;

	public UnityEvent<string> ItemPickedUp;

	const float GravityStrength = 10;

	public float JumpPower = 5;
	public float MoveSpeed = 5;
	public readonly ItemStore Inventory = new();
	public Transform ViewCamera;
	
	[SerializeField] protected InteractHand interactHand;
	[SerializeField] protected bool IsDisabled = false;
	protected Vector2 HorizontalInputDir;
	protected Vector2 HorizontalMoveDir;
	protected CharacterController CharControl;
	protected bool DoJump;

	private float _verticalVelocity;
	private Vector3 _finalMovement;
	private bool _collectedEnoughAlready = false;

	void Awake()
	{
		CharControl = GetComponent<CharacterController>();
	}

	void Update()
	{
		_finalMovement = Vector3.zero;

		ApplyHorizontalMovement();
		ApplyRotation();
		ApplyJump();
		ApplyGravity();

		CharControl.Move(_finalMovement);
	}

	// Apply a component along the XZ plane based on the horizontal input
	// Returns early, before making any changes to _finalMovement, if IsDisabled;
	private void ApplyHorizontalMovement()
	{
		if (IsDisabled) return;

		var deltaSpeed = Time.deltaTime * MoveSpeed;
		HorizontalMoveDir = Quaternion.Euler(0f, 0f, -ViewCamera.eulerAngles.y) * HorizontalInputDir;

		_finalMovement.x += HorizontalMoveDir.x * deltaSpeed;
		_finalMovement.z += HorizontalMoveDir.y * deltaSpeed;
	}

	// Rotates the character such that they are facing towards the direction of travel
	public void ApplyRotation()
	{
		if (HorizontalInputDir.magnitude < 0.1f) return;

		Quaternion toRotate = Quaternion.LookRotation(new Vector3(HorizontalMoveDir.x, 0f, HorizontalMoveDir.y));
		transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotate, 400f * Time.deltaTime);
	}

	// Applies upward velocity on the y axis, if a jump is queued (DoJump)
	// Returns early, before making any changes to _finalMovement, if IsDisabled;
	private void ApplyJump()
	{
		if (IsDisabled) return;

		if (!DoJump) return;
		DoJump = false;

		_verticalVelocity = JumpPower;
		_finalMovement.y += JumpPower * Time.deltaTime;
	}

	// Applies downward acceleration when airborne
	private void ApplyGravity()
	{
		if (CharControl.isGrounded && _verticalVelocity < 0f)
		{
			_verticalVelocity = -1;
		}
		else
		{
			_verticalVelocity -= GravityStrength * Time.deltaTime;
		}

		_finalMovement.y += _verticalVelocity * Time.deltaTime;
	}

	// Called upon picking up a collectable
	public virtual void OnCollect(Collectable collectable)
	{
		ItemPickedUp.Invoke(collectable.ItemName);
		Debug.Log("checking...");
		// Check whether we have collected enough
		// For now, we'll go for 12 geodes and 3 tonics
		if (Inventory.Has("geode", 12) && Inventory.Has("tonic", 3)) 
		{
			Debug.Log("Ready for cutscene!");
			if (_collectedEnoughAlready) return;
			CollectedEnoughItems();
			_collectedEnoughAlready = true;
		}
		else
		{
			_collectedEnoughAlready = false;
		}
	}

	public void DisableControl()
	{
		Debug.Log("My control has been disabled");
		IsDisabled = true;
	}

	public void EnableControl()
	{
		IsDisabled = false;
	}

	// INPUT LISTENER METHODS
	// Receives the Vector2 representing the players arrow key input, rotated by the view angle
	public void HorizontalInput(InputAction.CallbackContext ctx)
	{
		HorizontalInputDir = ctx.ReadValue<Vector2>();
	}

	// Receives the call to jump
	public void JumpInput(InputAction.CallbackContext ctx)
	{
		if (!ctx.started || !CharControl.isGrounded) return;
		DoJump = true;
	}

	// Receives the call to interact
	public void InteractInput(InputAction.CallbackContext ctx)
	{
		if (interactHand == null) return;
		if (!ctx.started) return;

		interactHand.SelectHighlightedObject();
	}

	// Receiver used for various UI bollocks
	public void EscInput(InputAction.CallbackContext ctx)
	{
		if (!ctx.started) return;
		UiEscape.Invoke();
	}
}
