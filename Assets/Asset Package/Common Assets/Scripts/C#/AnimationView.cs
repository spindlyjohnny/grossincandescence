using UnityEngine;
using System.Collections;

public class AnimationView : MonoBehaviour {
	private AnimationClip[] clips;
	private int i = 0;
	private int count = 0;
	public float fadeTime = 0.1f;

	void Start() {
		clips = (AnimationClip[])Resources.LoadAll<AnimationClip>("Animations/");
		Debug.Log ("Clips loaded: " + clips.Length + this.gameObject.name);

		foreach(AnimationClip c in clips) {
			GetComponent<Animation>().AddClip(c, c.name);
		}

		count = clips.Length;
		i = 0;
		GetComponent<Animation>().CrossFade (clips [i].name, fadeTime);
	}

	void Update() {
		if(GetComponent<Animation>()[clips [i].name].time / GetComponent<Animation>()[clips [i].name].length > 0.9f) {
			i += 1;

			if(i >= count) {
				i = 0;
			}

			GetComponent<Animation>().CrossFade(clips [i].name, fadeTime);
		}
	}
}