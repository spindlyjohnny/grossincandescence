using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobotLogic : MonoBehaviour {
	public string[] actions;
	public int indexAction;
	public Text statusGUI;
	public float t = 0;
	public float speed = 1.0f;
	private CharacterController robotController;
	private float jumpSpeed = 3;
	private bool flagIdle = false;
	private Vector3 moveDirection = Vector3.zero;
	private int boostIncr = 1;
	private bool canAnimate;
	private int gravity = 15;
	private float nextLoad = 0.0f;
	private float rate = 5.0f;
	private bool run = false;
	private float rotateY;
	private Vector3 angle;

	void Start() {
		indexAction = 0;
		if(statusGUI) statusGUI.text = actions[indexAction];
		robotController = GetComponent<CharacterController>();
		GetComponent<Animation>().wrapMode = WrapMode.Loop;
		GetComponent<Animation>()["idleTap"].wrapMode = WrapMode.Once;
	}

	void Update() {
		if(Input.GetButtonDown("Boost")) {
			run = true;
		} else if (Input.GetButtonUp("Boost")) {
			run = false;
		}

		if(!Input.GetButtonDown("Action")) {
			canAnimate = true;
			t = GetComponent<Animation>()[actions[indexAction]].clip.length + Time.time;
		}

		if(Input.GetButtonDown("Switch")) {
			indexAction = (indexAction + 1) % actions.Length;
			if(statusGUI) statusGUI.text = actions[indexAction];
		}

		if(robotController.isGrounded == true) {
			if(Input.GetAxis("Vertical") > 0.02 && !Input.GetKey("left ctrl")) {
				GetComponent<Animation>()["walk"].speed = 1;
				if(run) {
					GetComponent<Animation>().CrossFade("run");
					GetComponent<Animation>()["run"].speed = 1;
					boostIncr = 3;
				} else {
					GetComponent<Animation>().CrossFade("walk");
					boostIncr = 1;
				}
			} else if(Input.GetAxis("Vertical") < -0.02 && !Input.GetKey("left ctrl")) {
				GetComponent<Animation>()["walk"].speed = -1;
				GetComponent<Animation>().CrossFade("walk");
			} else {
				if(Input.GetButton("Action") && canAnimate) {
					DoAction();
				}
				else {
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
				GetComponent<Animation>().CrossFade("jump");
				GetComponent<Animation>()["jump"].speed = 2;
				moveDirection.y = jumpSpeed;
			}
		}

		if (Input.GetButtonDown("Action") && canAnimate) {
			DoAction();
		}

		angle = transform.eulerAngles;
		angle.y += Input.GetAxis("Horizontal") * 5; // Use keyboard to control character turning
		transform.eulerAngles = angle;
		rotateY = (Input.GetAxis("Mouse X") * 300) * Time.deltaTime; // Use mouse horizontal position to control character turning
		robotController.transform.Rotate(0, rotateY, 0);
		moveDirection.y -= gravity * Time.deltaTime;
		robotController.Move(moveDirection * Time.deltaTime);
	}

	void IdleAnimation() {
		if(Time.time > nextLoad) {
			if(flagIdle) {
				flagIdle = false;
			} else {
				flagIdle = true;
			}

			nextLoad = Time.time + rate;
		}

		if(flagIdle) {
			GetComponent<Animation>().CrossFade("idle");
		} else {
			GetComponent<Animation>().CrossFade("idleTap");
			if(!GetComponent<Animation>().IsPlaying("idleTap")) {
				flagIdle = true;
			}
		}
	}

	void DoAction() {
		GetComponent<Animation>().CrossFade(actions[indexAction]);

		if(Time.time > t - 0.5 && canAnimate) {
			GetComponent<Animation>().CrossFade(actions[indexAction]);
			canAnimate = false;
		}
	}
}