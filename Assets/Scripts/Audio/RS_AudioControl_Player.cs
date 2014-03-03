using UnityEngine;
using System.Collections;

public class RS_AudioControl_Player : MonoBehaviour {

	public AudioSource bgm;
	private bool destroySelf = false;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(this.gameObject);

		//plays audio clip tied to AudioSource if not currently playing
		if (!bgm.isPlaying)
			bgm.Play();
	}
	
	// Update is called once per frame
	void Update () {
		//If set to destroy (destroyAudio = true), stops the sound and destroys self
		if (destroySelf){
			bgm.Stop();
			Destroy(this.gameObject);
		}
	}

	/*
	 * RemoveObject
	 * Function: Allows for other objects to destroy this object
	 * */
	public void RemoveObject(){
		destroySelf = true;
	}
}
