using UnityEngine;
using System.Collections;

public class RS_TriggerTest : MonoBehaviour {

	public AudioSource sfx;
	private bool playSound, hasPlayed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (playSound && !hasPlayed){
			sfx.Play();
			hasPlayed = true;
		}
	}

	void OnTriggerEnter(Collider collision){
		if (collision.gameObject.tag == "Player")
			if (!playSound)
				playSound = true;
	}
}
