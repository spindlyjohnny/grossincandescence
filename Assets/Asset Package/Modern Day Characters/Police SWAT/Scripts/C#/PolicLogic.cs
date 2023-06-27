using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PolicLogic : MonoBehaviour {
	public string[] actions;
	public int indexAction;
	public Text statusGUI;
	public float speed = 1.5f;
	private CharacterController policeController = null;
	private float jumpSpeed = 5;
	private Vector3 moveDirection = Vector3.zero;
	private int boostIncr = 1;
	private bool canAnimate;
	private float gravity = 15;
	private bool run = false;
	private float rotateY;
	private Vector3 angle;

	void Start() {
		indexAction = 0;
		if(statusGUI) statusGUI.text = actions[indexAction];
		policeController = GetComponent<CharacterController>();
		GetComponent<Animation>().wrapMode = WrapMode.Loop;
		GetComponent<Animation>()["Standing and aiming"].wrapMode = WrapMode.Once;
		GetComponent<Animation>()["Crouching and aiming"].wrapMode = WrapMode.Once;
		GetComponent<Animation>()["Standing with single fire"].wrapMode = WrapMode.Once;
		GetComponent<Animation>()["Idle crouching with single fire"].wrapMode = WrapMode.Once;
	}

	void Update() {
		if(Input.GetButtonDown("Boost")) {
			run = true;
		} else if(Input.GetButtonUp("Boost")) {
			run = false;
		}

		if(!Input.GetButtonDown("Action")) {
			canAnimate = true;
		}

		if(Input.GetButtonDown("Switch")) {
			indexAction++;
			if(indexAction == actions.Length)
				indexAction = 0;
			if(statusGUI) statusGUI.text = actions[indexAction];
		}

		if(policeController.isGrounded == true) {
			if(Input.GetAxis("Vertical") > 0.02 && !Input.GetKey("left ctrl")) {
				GetComponent<Animation>()["Walking"].speed = 1;
				if(run) {
					GetComponent<Animation>().CrossFade("Running");
					GetComponent<Animation>()["Running"].speed = 1;
					boostIncr = 3;
				} else {
					GetComponent<Animation>().CrossFade("Walking");
					boostIncr = 1;
				}
			} else if (Input.GetAxis("Vertical") < -0.02 && !Input.GetKey("left ctrl")) {
				GetComponent<Animation>()["Walking"].speed = -1;
				GetComponent<Animation>().CrossFade("Walking");
			} else {
				if(Input.GetButton("Action") && canAnimate) {
					DoAction();
				} else {
					IdleAnimation();
					boostIncr = 1;
				}
			}

			if(!Input.GetButton("Action")) {
				moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
				moveDirection = transform.TransformDirection(moveDirection);
				moveDirection *= boostIncr * speed;
			}

			if(Input.GetButtonDown("Jump")) {
				moveDirection.y = jumpSpeed;
				GetComponent<Animation>()["Jumping"].speed = 2;
				GetComponent<Animation>().CrossFade("Jumping");
			}
		}

		if (Input.GetButtonDown("Action") && canAnimate) {
			DoAction();
		}

		angle = transform.eulerAngles;
		angle.y += Input.GetAxis("Horizontal") * 5;
		transform.eulerAngles = angle; // Use keyboard to control character turning
		rotateY = (Input.GetAxis("Mouse X") * 300) * Time.deltaTime; // Use mouse horizontal position to control character turning
		policeController.transform.Rotate(0, rotateY, 0);
		moveDirection.y -= gravity * Time.deltaTime;
		policeController.Move(moveDirection * Time.deltaTime);
	}

	void IdleAnimation() {
		GetComponent<Animation>().CrossFade("Standby");
	}

	void DoAction() {
		GetComponent<Animation>().CrossFade(actions[indexAction]);
		canAnimate = false;
	}
}