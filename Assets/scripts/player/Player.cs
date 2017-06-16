using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(CharacterController))]
[RequireComponent (typeof(GunController))]
public class Player : LivingEntity {
    public float moveSpeed = 5F;
	public uint maxJumps;
	public float minJumpHeight = 1F;
	public float maxJumpHeight = 4F;
	public float jumpApexTime = 0.4F;

	float gravity;
	uint jumps;
	float minJumpVelocity;
	float maxJumpVelocity;
	public float accelerationInAir = 0.4F;
	public float accelerationOnGround = 0.1F;

	CharacterController controller;
	GunController gunController;

	Vector3 v;
	[HideInInspector]
	public Vector3 velocity { get{ return v; } }
	float smooth;

	public float minMouseDistance = 1F;
	public LayerMask mouseMask;

	Camera cam;
	Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
	// Use this for initialization
	protected override void Start () {
		base.Start();

		gravity = (-2 * maxJumpHeight) / Mathf.Pow (jumpApexTime, 2);
		maxJumpVelocity = Mathf.Abs(gravity) * jumpApexTime;
		minJumpVelocity = Mathf.Sqrt (2 * Mathf.Abs (gravity) * minJumpHeight);

		controller = GetComponent<CharacterController>();
		gunController = GetComponent<GunController>();
		gunController.SetColor(color);
		cam = Camera.main;
	}

	void Update () {
		//Movement
		if (controller.isGrounded && jumps <= 0) jumps = maxJumps;
		CalculateVelocity();
		controller.Move(v * Time.deltaTime);

		//Looking
		AdjustPlayerRotation();

		//Weapon Input
		if(Input.GetMouseButtonDown(0)) gunController.OnTriggerPress();
		if(Input.GetMouseButton(0)) gunController.OnTriggerHold();
		if(Input.GetMouseButtonUp(0)) gunController.OnTriggerRelease();
		SwitchColor(Input.GetAxisRaw("Mouse ScrollWheel"));
	}

	void CalculateVelocity() {
		Vector3 moveInput = (new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"))).normalized;
		float targetSpeed = moveInput != Vector3.zero? moveSpeed : 0;
		float acceleration = controller.isGrounded? accelerationOnGround : accelerationInAir;

		v.x = Mathf.Lerp(v.x, moveInput.x * targetSpeed, Time.deltaTime * acceleration);
		v.z = Mathf.Lerp(v.z, moveInput.z * targetSpeed, Time.deltaTime * acceleration);
		 
		if (Input.GetButtonDown("Jump") && canJump()) v.y = maxJumpVelocity;
		else if (Input.GetButtonUp("Jump") && v.y > minJumpVelocity) v.y = minJumpVelocity;

		if (!controller.isGrounded) v.y += gravity * Time.deltaTime;
	}

	bool canJump() {
		if (jumps > 0) {
			jumps--;
			return true;
		}
		return false;
	}

	void AdjustPlayerRotation(){
		Ray ray = cam.ScreenPointToRay(Input.mousePosition);
		float distance;
		RaycastHit hit;

		if (groundPlane.Raycast (ray, out distance)) {
			Vector3 point = ray.GetPoint(distance);
			if (Physics.Raycast (ray, out hit, distance, mouseMask)) point = hit.point;
			
			Transform gunTransform = gunController.weaponHold;
			Vector3 playerPoint = new Vector3(point.x, transform.position.y, point.z);

			if (Vector3.Distance(transform.position, playerPoint) < minMouseDistance)
				point = transform.position + ((playerPoint - transform.position).normalized * minMouseDistance);

			Debug.DrawLine(Camera.main.transform.position, point);
			transform.LookAt(new Vector3(point.x, transform.position.y, point.z));
			gunTransform.LookAt(new Vector3 (point.x, gunTransform.position.y, point.z));
		}
	}

	void SwitchColor(float scrollAxis) {
		if (scrollAxis > 0) {
			if (color == Manager.COLORS.COLORA) color = Manager.COLORS.COLORC;
			else if (color == Manager.COLORS.COLORB) color = Manager.COLORS.COLORA;
			else if (color == Manager.COLORS.COLORC) color = Manager.COLORS.COLORB;
			gunController.SetColor(color);
		}
		if (scrollAxis < 0) {
			if (color == Manager.COLORS.COLORA) color = Manager.COLORS.COLORB;
			else if (color == Manager.COLORS.COLORB) color = Manager.COLORS.COLORC;
			else if (color == Manager.COLORS.COLORC) color = Manager.COLORS.COLORA;
			gunController.SetColor(color);
		}
	}
}
