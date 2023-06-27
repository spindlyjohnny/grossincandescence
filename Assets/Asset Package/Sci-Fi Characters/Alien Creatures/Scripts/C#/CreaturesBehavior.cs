using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreaturesBehavior : MonoBehaviour {
	public float speed = 8000;
	//public GameObject root;
	private int randNum;
	private float timer;
	public GameObject myRoot;
	public GameObject myBodyZone;
	public GameObject myDangerZone;
	public float health = 3;
	public float damage = 1;
	//public ParticleSystem bloodParticle;
	private int state = 1; // 1 for run, 2 for fight, 3 for dead

	void Start() {
		Destroy(myRoot, 40);
	}

	void FixedUpdate() {
		if(health < 0)
			state = 3;

		if(state == 3) { // Died
			timer += Time.deltaTime;
			myRoot.GetComponent<Collider>().enabled = false;
			Destroy(myRoot.GetComponent<Rigidbody>());

			//bloodParticle.emissionRate=0;	

			if(!myRoot.GetComponent<Animation>().IsPlaying("die1") && !myRoot.GetComponent<Animation>().IsPlaying("die2")) {
				randNum = Random.Range(1, 3);

				if(randNum == 1)
					myRoot.GetComponent<Animation>().CrossFade("die1");
				if(randNum == 2)
					myRoot.GetComponent<Animation>().CrossFade("die2");
			}

			if(timer > 2)
				myRoot.transform.Translate(0, -0.2f * Time.deltaTime, 0);

			if(timer > 10)
				Destroy(myRoot);
		}

		if(state == 1) { // Running
			//bloodParticle.emissionRate=0;
			myRoot.GetComponent<Rigidbody>().AddRelativeForce(0, 0, speed * Time.deltaTime * (7 - myRoot.GetComponent<Rigidbody>().velocity.magnitude));
			myRoot.GetComponent<Rigidbody>().drag = 0.5f;

			if(!myRoot.GetComponent<Animation>().IsPlaying("run")) {
				myRoot.GetComponent<Animation>().CrossFade("run");
			}
		}

		if(state == 2) { // Attacking
			//bloodParticle.emissionRate = 5;
			state = 1;

			if(!myRoot.GetComponent<Animation>().IsPlaying("attack1") && !myRoot.GetComponent<Animation>().IsPlaying("attack2")) {
				randNum = Random.Range(1, 3);

				if(randNum == 1)
					myRoot.GetComponent<Animation>().CrossFade("attack1");

				if(randNum == 2)
					myRoot.GetComponent<Animation>().CrossFade("attack2");
			}
		}
	}

	void OnTriggerStay(Collider other) {
		if(other.tag == "bodyZone" && state != 3) {
			other.transform.Find("controller").GetComponent<CreaturesBehavior>().health -= damage * Time.deltaTime;
			myRoot.GetComponent<Rigidbody>().drag = 10;

			state = 2;
		}
	}
}