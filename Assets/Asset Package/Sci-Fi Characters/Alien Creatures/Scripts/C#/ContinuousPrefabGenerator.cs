using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuousPrefabGenerator : MonoBehaviour {
	public GameObject[] createThis; // List of possible prefabs
	public float thisManyTimesPerSec = 1;
	public float xWidth; // Area where prefabs will be generated
	public float yWidth;
	public float zWidth;
	public float xRotMax; // Maximum rotation of each prefab
	public float yRotMax = 180;
	public float zRotMax;
	private int randNum;
	private float x_cur; // Random placement process
	private float y_cur;
	private float z_cur;
	private float xRotCur; // For random rotation process
	private float yRotCur;
	private float zRotCur;
	private float timeCounter;
	private int effectCounter;
	private float trigger; // Trigger at interwals to generate a prefab
	
	void Start() {
		if(thisManyTimesPerSec < 0.1)
			thisManyTimesPerSec = 0.1f; // Avoid division with zero and negative numbers

		trigger = 1 / thisManyTimesPerSec; // Define the intervals of time of the prefab generation
	}

	void Update() {
		timeCounter += Time.deltaTime;

		if(timeCounter > trigger) {
			randNum = (int)Mathf.Floor(Random.value * createThis.Length); // Randomize prefab to create

			// Random location
			x_cur = transform.localPosition.x + (Random.value * xWidth) - (xWidth * 0.5f);
			y_cur = transform.localPosition.y + (Random.value * yWidth) - (yWidth * 0.5f);
			z_cur = transform.localPosition.z + (Random.value * zWidth) - (zWidth * 0.5f);

			// Random rotation
			xRotCur = transform.rotation.x + (Random.value * xRotMax * 2) - (xRotMax);
			yRotCur = transform.rotation.y + (Random.value * yRotMax * 2) - (yRotMax);
			zRotCur = transform.rotation.z + (Random.value * zRotMax * 2) - (zRotMax);

			GameObject justCreated = (GameObject)Instantiate(createThis[randNum], new Vector3(x_cur, y_cur, z_cur), transform.rotation); // Create the prefab
			justCreated.transform.Rotate(xRotCur, yRotCur, zRotCur);

			timeCounter -= trigger;
		}
	}
}