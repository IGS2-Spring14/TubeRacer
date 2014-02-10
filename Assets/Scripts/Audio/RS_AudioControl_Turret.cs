using UnityEngine;
using System.Collections;

public class RS_AudioControl_Turret : MonoBehaviour {

	/*
	 * Begin:
	 * Ensures that a duplicate object playing this sound doesn't exist.
	 * */
	private static RS_AudioControl_Turret instance = null;
	
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
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
