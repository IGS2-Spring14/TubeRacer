using UnityEngine;
using System.Collections;

public class RS_3DButtonTrigger : MonoBehaviour {

	public string desiredScene;
	public bool isLevelRestartButton;

	// Use this for initialization
	void Start () {
		//Sets target scene to current level is set to be a level restart button
		if (isLevelRestartButton)
			desiredScene = Application.loadedLevelName;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider collision) { 
		if (collision.gameObject.CompareTag ("Gun")) {
			//Enter desired action here
			if (isLevelRestartButton)
				print ("scene would reload here");

			print ("has been hit");

			Application.LoadLevel(desiredScene);
		}

	}
}
