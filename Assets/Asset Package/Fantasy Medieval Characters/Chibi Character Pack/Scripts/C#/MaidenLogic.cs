using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaidenLogic : MonoBehaviour {
	//public String[] actions;
	//public int indexAction;
	//public GUIText statusGUI;
	public float speed = 1.5f;
	private CharacterController maidenController;
	private float jumpSpeed = 5;
	private Vector3 moveDirection = Vector3.zero;
	private int boostIncrement = 1;
	private bool canAnimate;
	private float gravity = 15;
	private bool run = false;
	private Vector3 angle;
	//private var rotateY : float;

	void Start() {
		//	indexAction = 0;
		//	statusGUI.text = actions[indexAction];
		maidenController = GetComponent<CharacterController>();
		GetComponent<Animation>().wrapMode = WrapMode.Loop;
		GetComponent<Animation>()["jump-01"].wrapMode = WrapMode.Once;
		GetComponent<Animation>()["attack-02"].wrapMode = WrapMode.Once;
		//	GetComponent.<Animation>()["Crouching and aiming"].wrapMode = WrapMode.Once;
		//	GetComponent.<Animation>()["Standing with single fire"].wrapMode = WrapMode.Once;
		//	GetComponent.<Animation>()["Idle crouching with single fire"].wrapMode = WrapMode.Once;
	}

	void Update() {
		print(canAnimate);
		if(Input.GetButtonDown("Boost")) {
			run = true;
		} else if(Input.GetButtonUp("Boost")) {
			run = false;
		}

		if(!Input.GetButtonDown("Attack1")) {
			canAnimate = true;
		}

		//	if(Input.GetButtonDown("Switch")) {
		//		indexAction++;
		//		if(indexAction == actions.length)
		//			indexAction = 0;
		//		statusGUI.text = actions[indexAction];
		//	}

		if(maidenController.isGrounded == true) {
			if(Input.GetAxis("Vertical") > 0.02 && !Input.GetKey("left ctrl")) {
				GetComponent<Animation>()["walk-01"].speed = 1;
				if(run) {
					GetComponent<Animation>().CrossFade("run-01");
					GetComponent<Animation>()["run-01"].speed = 1;
					boostIncrement = 3;
				} else {
					GetComponent<Animation>().CrossFade("walk-01");
					boostIncrement = 1;
				}
			} else if (Input.GetAxis("Vertical") < -0.02 && !Input.GetKey("left ctrl")) {
				GetComponent<Animation>()["walk-01"].speed = -1;
				GetComponent<Animation>().CrossFade("walk-01");
			} else {
				if(Input.GetButton("Attack1") && canAnimate) {
					DoAttack1();
				} else if(Input.GetButton("Attack2") && canAnimate) {
					DoAttack2();
				} else {
					IdleAnimation();
					boostIncrement = 1;
				}
			}

			if(!Input.GetButtonDown("Attack1")) {
				moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
				moveDirection = transform.TransformDirection(moveDirection);
				moveDirection *= boostIncrement * speed;
			}

			if(Input.GetButtonDown("Jump")) {
				moveDirection.y = jumpSpeed;
				GetComponent<Animation>()["jump-01"].speed = 2;
				GetComponent<Animation>().CrossFade("jump-01");
			}
		}

		//if(Input.GetButtonDown("Attack1") && canAnimate) {
		//	DoAction();
		//}

		angle = transform.eulerAngles;
		angle.y += Input.GetAxis("Horizontal") * 5; // Use keyboard to control character turning
		transform.eulerAngles = angle;
		//rotateY = (Input.GetAxis("Mouse X") * 300) * Time.deltaTime; // Use mouse horizontal position to control character turning
		//maidenController.transform.Rotate(0, rotateY, 0);
		moveDirection.y -= gravity * Time.deltaTime;
		maidenController.Move(moveDirection * Time.deltaTime);
	}

	void IdleAnimation() {
		GetComponent<Animation>().CrossFade("idle-03");
	}

	void DoAttack1() {
		GetComponent<Animation>()["attack-02"].speed = 1;
		GetComponent<Animation>().CrossFade("attack-02");
		canAnimate = false;
	}

	void DoAttack2() {
		GetComponent<Animation>()["attack-06"].speed = 1;
		GetComponent<Animation>().CrossFade("attack-06");
		canAnimate = false;
	}
}