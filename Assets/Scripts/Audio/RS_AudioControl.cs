using UnityEngine;
using System.Collections;

public class RS_AudioControl : MonoBehaviour {

	public AudioSource bgm;
	private static bool destroyAudio = false;

	/*
	 * Begin:
	 * Ensures that a duplicate object playing this sound doesn't exist.
	 * */
	private static RS_AudioControl instance = null;

	void Awake() {
		if (instance != null && instance != this) {
			Destroy(this.gameObject);
			return;
		} else {
			instance = this;
		}
		DontDestroyOnLoad(this.gameObject);
	}
	/*
	 * End
	 * */

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(this.gameObject);

		//tells self not to destroy self
		destroyAudio = false;

		//plays audio clip tied to AudioSource if not currently playing
		if (!bgm.isPlaying)
			bgm.Play();
	}
	
	// Update is called once per frame
	void Update () {
		//If set to destroy (destroyAudio = true), stops the sound and destroys self
		if (destroyAudio){
			bgm.Stop();
			Destroy(this.gameObject);
		}
	}

	/*
	 * RemoveObject
	 * Function: Allows for other objects to destroy this object
	 * */
	public void RemoveObject(){
		destroyAudio = true;
	}
}
