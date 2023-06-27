using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimSpeedRandomizer : MonoBehaviour {
	public float minSpeed = 0.7f;
	public float maxSpeed = 1.5f;
	
	void Start() {
		GetComponent<Animation>()[GetComponent<Animation>().clip.name].speed = Random.Range(minSpeed, maxSpeed);
	}
}