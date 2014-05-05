using UnityEngine;
using System.Collections;

public class RS_ButtonTrigger : MonoBehaviour {

	public string targetScene;
	public bool isRestartButton;

	void Awake () {
		if (isRestartButton)
			targetScene = Application.loadedLevelName;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider collision) {
		if (collision.gameObject.tag == "Gun")
			Application.LoadLevel (targetScene);
	}
}
