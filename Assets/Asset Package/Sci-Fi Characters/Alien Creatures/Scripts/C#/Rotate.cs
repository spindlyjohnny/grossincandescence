using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {
	public float rotationSpeedX = 0;
	public float rotationSpeedY = 15;
	public float rotationSpeedZ = 0;

	void Update() {
		transform.Rotate(new Vector3(rotationSpeedX, rotationSpeedY, rotationSpeedZ) * Time.deltaTime);
	}
}